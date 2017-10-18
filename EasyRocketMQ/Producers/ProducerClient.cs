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
        /// 使用oneway方式发送
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="tag">标签</param>        
        /// <param name="key">消息key, 要做到局唯一</param>
        public void SendOnewayMessage(string topic, string content, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);
            producer.sendOneway(message);
        }

        /// <summary>
        /// 发送单向定时消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="deliveryTime">投送时间</param>
        /// <param name="tag">标签</param>
        /// <param name="key">消息Key</param>
        public void SendOnewayAndTimingMessage(string topic, string content, DateTime deliveryTime, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);
            message.setStartDeliverTime(deliveryTime.ToTimestamp());

            producer.sendOneway(message);
        }

        /// <summary>
        /// 发送定时消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="deliveryTime">投送时间</param>
        /// <param name="tag">标签</param>
        /// <param name="key">消息Key</param>
        public string SendTimingMessage(string topic, string content, DateTime deliveryTime, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);
            message.setStartDeliverTime(deliveryTime.ToTimestamp());

            var result = producer.send(message);

            return result.getMessageId();
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="key">消息key, 要做到局唯一</param>
        /// <param name="tag">标签</param>
        public string SendMessage(string topic, string content, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);

            var result = producer.send(message);

            return result.getMessageId();
        }
    }
}