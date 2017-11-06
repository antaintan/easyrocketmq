对阿里云.net 客户端ons的进一步封装，ons是基于cpp dll的PInvoke调用封装，使用起来非常不方便，因此对ons进一步封装，方程序调用便.
 示例代码:
 1. 生产消息
        private static ProducerClient producerClient = new ProducerClient(AccessKeyId, AccessKeySecret, ProducerId);

        private static void Main(string[] args)
        {
            producerClient.Start();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var taskList = new List<Task>();
            for (int threadIndex = 1; threadIndex <= ProducerThreadCount; threadIndex++)
            {
                // 生产消费
                var task = Task.Factory.StartNew(() => {
                    for (int messageIndex = 1; messageIndex <= MessageCountPerThread; messageIndex++)
                    {
                        string content = "线程ID=" + Thread.CurrentThread.ManagedThreadId + ", 我要测试rocketmq message";
                        //producerClient.SendMessage(ShardingKey, Topic, content, Tag);
                        producerClient.SendMessage(Topic, content, Tag);

                        Console.WriteLine(content);
                    }
                }, TaskCreationOptions.LongRunning);

                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());
            stopWatch.Stop();

            // 一定要关闭，不然会有内存泄漏
            producerClient.Shutdown();

            Console.WriteLine("发送消息：{0}条， 使用时间{1}毫秒", MessageCountPerThread * ProducerThreadCount, stopWatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
 2. 消费消息
        private static PushConsumerClient consumerClient = new PushConsumerClient(AccessKeyId, AccessKeySecret, Topic, ConsumerId, SubExpression);

        private static int count = 0;

        private class MyMsgListener : DefaultMessageListener
        {
            public override ons.Action consume(Message message, ConsumeContext context)
            {
                Console.WriteLine("消息序号: {0}, 当前线程ID = {1}, 内容为： {2}", ++count, Thread.CurrentThread.ManagedThreadId, message.getBody());
                return ons.Action.CommitMessage;
            }
        }

        private class MyMsgOrderListener : DefaultMessageOrderListener
        {
            public override OrderAction consume(Message message, ConsumeOrderContext context)
            {
                Console.WriteLine("消息序号: {0}, 当前线程ID = {1}, 内容为： {2}", ++count, Thread.CurrentThread.ManagedThreadId, message.getBody());
                return ons.OrderAction.Success;
            }
        }

        private static void Main(string[] args)
        {
            var listener = new MyMsgListener();
            consumerClient.setMessageListener(listener);
            consumerClient.Start();

            Console.ReadLine();
            consumerClient.Shutdown();
        }