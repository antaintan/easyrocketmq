using ons;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 有序消息生产客户端
    /// </summary>
    public class OrderProducerClient : ProducerClientBase
    {
        /// <summary>
        /// 有序消息生产者
        /// </summary>
        private OrderProducer producer;

        public OrderProducerClient(string accessKeyId, string accessKeySecret, string producerId)
            : base(accessKeyId, accessKeySecret, producerId)
        {
        }

        public override void Start()
        {
            producer = ONSFactory.getInstance().createOrderProducer(this.FactoryProperty);
            producer.start();
        }

        public override void Shutdown()
        {
            producer.shutdown();
        }

        /// <summary>
        /// 发送分区顺序消息
        /// </summary>
        /// <param name="shardingKey">分区Key</param>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="tag">消息标签</param>
        /// <param name="key">消息Key</param>
        /// <returns></returns>
        public string SendMessage(string shardingKey, string topic, string content, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);

            var sendResult = producer.send(message, shardingKey);
            return sendResult.getMessageId();
        }
    }
}