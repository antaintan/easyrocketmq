using EasyRocketMQ.Consumers;
using ons;
using System;
using System.Threading;

namespace EasyRocketMQ.Sample.Consumer
{
    internal class Program
    {
        // 以下内容换成自己在阿里云的相关信息
        private static readonly string AccessKeyId = "xxxxxxxxxxxxxxxxxx";

        private static readonly string AccessKeySecret = "xxxxxxxxxxxxxxxxxx";

        private static readonly string Topic = "xxxxxxxxxxxxxxxxxx";

        private static readonly string ConsumerId = "xxxxxxxxxxxxxxxxxx";

        private const string SubExpression = "xxxxxxxxxxxxxxxxxx";

        /// <summary>
        /// 静态全局变量，保持只一个消费者TCP连接
        /// </summary>
        private static PushConsumerClient consumerClient = new PushConsumerClient(AccessKeyId, AccessKeySecret, Topic, ConsumerId, SubExpression);

        //private static OrderConsumerClient consumerClient = new OrderConsumerClient(AccessKeyId, AccessKeySecret, Topic, ConsumerId, SubExpression);

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

            //var listener = new MyMsgOrderListener();
            //consumerClient.setMessageListener(listener); 

            consumerClient.Start();

            Console.ReadLine();
            consumerClient.Shutdown();
        }
    }
}