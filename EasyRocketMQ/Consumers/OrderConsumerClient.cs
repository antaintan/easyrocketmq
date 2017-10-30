using ons;
using System;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 顺序消息消费者客户端
    /// </summary>
    public class OrderConsumerClient : ConsumerClientBase
    {
        /// <summary>
        /// 真正的消息消费者
        /// </summary>
        private OrderConsumer consumer;

        /// <summary>
        /// 消息监听器
        /// </summary>
        private MessageOrderListener listener;

        public OrderConsumerClient(string accessKeyId, string accessKeySecret, string topic, string consumerId, string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret, topic, consumerId, subExpression, consumerThreadCount)
        {
        }

        public override void Shutdown()
        {
            consumer.shutdown();
        }

        public override void Start()
        {
            if (this.listener == null)
            {
                throw new NotFoundMessageListenerException();
            }

            consumer = ONSFactory.getInstance().createOrderConsumer(this.FactoryProperty);
            consumer.subscribe(Topic, SubExpression, listener);
            consumer.start();
        }

        /// <summary>
        /// 设置消息监听器
        /// </summary>
        /// <param name="listener"></param>
        public void setMessageListener(MessageOrderListener listener)
        {
            this.listener = listener;
        }
    }
}