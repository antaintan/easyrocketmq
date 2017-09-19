using ons;
using System;

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
        /// 发送顺序消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="tag">消息标签</param>
        public override void SendMessage(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "")
        {
            var message = new Message(topic, tag, content);
            message.setKey(tag + "_" + Guid.NewGuid().ToString("N"));

            try
            {
                var sendResult = producer.send(message, shardingKey);
                // TODO: write log here, sendResult.getMessageId
            }
            catch (Exception ex)
            {
                // TODO: write exception here
            }
        }

        public override void SendMessageByOneway(string topic, string content, string tag = "", DateTime? deliveryTime = default(DateTime?), string shardingKey = "")
        {
            throw new NotSupportedException("顺序消息不支持Oneway发送");
        }
    }
}