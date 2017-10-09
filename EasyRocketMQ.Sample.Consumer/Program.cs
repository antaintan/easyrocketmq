using EasyRocketMQ.Consumers;
using System;
using System.Threading;

namespace EasyRocketMQ.Sample.Consumer
{
    internal class Program
    {
        // 以下内容换成自己在阿里云的相关信息
        private static readonly string AccessKeyId = "LTAIspikO0gRSqC5";

        private static readonly string AccessKeySecret = "Y3l9MVqQsdM2OnOTs7v5dalcPNZXGJ";

        private static readonly string Topic = "testpay";

        private static readonly string ConsumerId = "CID_testpay_consumer1";

        /// <summary>
        /// 静态全局变量，保持只一个消费者TCP连接
        /// </summary>
        private static OrderConsumerClient consumerClient = new OrderConsumerClient(AccessKeyId, AccessKeySecret, Topic, ConsumerId);

        private static void Main(string[] args)
        {
            consumerClient.SetConsumeFunc((message, context) => {
                Console.WriteLine("当前线程ID = {0}, 内容为： {1}", Thread.CurrentThread.ManagedThreadId, message.getBody());
                return ons.OrderAction.Success;
            });

            consumerClient.Start();

            Console.ReadLine();
            consumerClient.Shutdown();
        }
    }
}