using ons;
using System;
using System.Text;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 无序消息生产者客户端
    /// </summary>
    public class ProducerClient : ProducerClientBase
    {
        /// <summary>
        /// 内部真正的生产者
        /// </summary>
        private Producer producer;

        public ProducerClient(string accessKeyId, string accessKeySecret, string producerId)
            : base(accessKeyId, accessKeySecret, producerId)
        {
        }

        public override void Start()
        {
            producer = ONSFactory.getInstance().createProducer(this.FactoryProperty);
            producer.start();
        }

        public override void Shutdown()
        {
            producer.shutdown();
        }

        /// <summary>
        /// 生产顺序消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="tag">标签</param>
        /// <param name="deliveryTime">投送时间</param>
        /// <param name="shardingKey">分区key</param>
        public override void SendMessage(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "")
        {
            Send(topic, content, tag, deliveryTime, shardingKey);
        }

        /// <summary>
        /// 生产顺序消息并使用oneway方式发送
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="tag">标签</param>
        /// <param name="deliveryTime">投送时间</param>
        /// <param name="shardingKey">分区key</param>
        public override void SendMessageByOneway(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "")
        {
            Send(topic, content, tag, deliveryTime, shardingKey, true);
        }

        /// <summary>
        /// 内容消息发送实现方法
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="tag">标签</param>
        /// <param name="deliveryTime">投送时间</param>
        /// <param name="shardingKey">分区key</param>
        /// <param name="isOneway">是否为oneway发送</param>
        private void Send(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "", bool isOneway = false)
        {
            var message = new Message(topic, tag, string.Empty);

            var bodyData = Encoding.UTF8.GetBytes(content);
            message.setBody(bodyData, bodyData.Length);

            message.setKey(tag + "_" + Guid.NewGuid().ToString("N"));

            if (deliveryTime.HasValue)
            {
                message.setStartDeliverTime((long)(deliveryTime.Value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            }

            try
            {
                if (isOneway)
                {
                    producer.sendOneway(message);
                }
                else
                {
                    var sendResult = producer.send(message);
                }

                // log here
            }
            catch (Exception ex)
            {
                // log exception here
            }
        }
    }
}