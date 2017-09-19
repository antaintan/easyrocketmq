using ons;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 消费者客户端(pull), 需要客户端维护offset, 比较复杂
    /// </summary>
    public class PullConsumerClient : ConsumerClientBase
    {
        /// <summary>
        /// 真正的消费者
        /// </summary>
        private PullConsumer consumer;

        public PullConsumerClient(string accessKeyId, string accessKeySecret, string consumerId, string topic, string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret, topic, consumerId, subExpression, consumerThreadCount)
        {
        }

        public override void Shutdown()
        {
            consumer.shutdown();
        }

        public override void Start()
        {
            consumer = new PullConsumer();
            consumer.start();
        }
    }
}