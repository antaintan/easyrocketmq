using ons;

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
        /// 消息监听器
        /// </summary>
        private MessageListener listener;

        public PushConsumerClient(string accessKeyId, string accessKeySecret, string topic, string consumerId,
                                     string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret, topic, consumerId, subExpression, consumerThreadCount)
        {
        }

        public override void Start()
        {
            if (this.listener == null)
            {
                throw new NotFoundMessageListenerException();
            }

            consumer = ONSFactory.getInstance().createPushConsumer(this.FactoryProperty);
            consumer.subscribe(Topic, SubExpression, listener);
            consumer.start();
        }

        public override void Shutdown()
        {
            consumer.shutdown();
        }

        /// <summary>
        /// 设置消息监听器
        /// </summary>
        /// <param name="listener"></param>
        public void setMessageListener(MessageListener listener)
        {
            this.listener = listener;
        }
    }
}