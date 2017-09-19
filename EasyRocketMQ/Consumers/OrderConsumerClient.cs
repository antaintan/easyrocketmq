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
        /// 真正的消息消息者
        /// </summary>
        private OrderConsumer consumer;

        /// <summary>
        /// 消费方法
        /// </summary>
        private Func<Message, ConsumeOrderContext, OrderAction> consumeFunc;

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
            consumer = ONSFactory.getInstance().createOrderConsumer(this.FactoryProperty);
            consumer.subscribe(Topic, SubExpression, new ConsumerMessageOrderListener(this.consumeFunc));
            consumer.start();
        }

        /// <summary>
        /// 设置消费消息的方法回调, 调用Start方法时，必需调用此方法来设置消费回调
        /// </summary>
        /// <param name="consumeFunc">消费方法</param>
        public void SetConsumeFunc(Func<Message, ConsumeOrderContext, OrderAction> consumeFunc)
        {
            this.consumeFunc = consumeFunc;
        }
    }
}