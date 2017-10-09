using ons;
using System;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 消费者客户端(push模式)
    /// </summary>
    public class PushConsumerClient : ConsumerClientBase
    {
        /// <summary>
        /// 真正的消费者
        /// </summary>
        protected PushConsumer consumer;

        /// <summary>
        /// 消费方法
        /// </summary>
        private Func<Message, ConsumeContext, ons.Action> consumeFunc;

        public PushConsumerClient(string accessKeyId, string accessKeySecret, string topic, string consumerId,
                                     string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret, topic, consumerId, subExpression, consumerThreadCount)
        {
        }

        public override void Start()
        {
            if (this.consumeFunc == null)
            {
                throw new Exception("没有设置消费方法，请调用SetConsumeFunc");
            }

            consumer = ONSFactory.getInstance().createPushConsumer(this.FactoryProperty);
            consumer.subscribe(Topic, SubExpression, new ConsumerMessageListener(this.consumeFunc));
            consumer.start();
        }

        public override void Shutdown()
        {
            consumer.shutdown();
        }

        /// <summary>
        /// 设置消费消息的方法回调, 调用Start方法时，必需调用此方法来设置消费回调
        /// </summary>
        /// <param name="consumeFunc">消费方法</param>
        public void SetConsumeFunc(Func<Message, ConsumeContext, ons.Action> consumeFunc)
        {
            this.consumeFunc = consumeFunc;
        }
    }
}