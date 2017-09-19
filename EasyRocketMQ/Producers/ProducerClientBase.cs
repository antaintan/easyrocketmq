using ons;
using System;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 生产者
    /// </summary>
    public abstract class ProducerClientBase : RocketClientBase
    {
        /// <summary>
        /// 生产者ID
        /// </summary>
        protected string ProducerId { get; private set; }

        public ProducerClientBase(string accessKeyId, string accessKeySecret, string producerId)
            : base(accessKeyId, accessKeySecret)
        {
            this.ProducerId = producerId;
            this.FactoryProperty.setFactoryProperty(ONSFactoryProperty.ProducerId, producerId);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="topic">消息主题</param>
        /// <param name="content">消息内容</param>
        /// <param name="tag">消息标签</param>
        /// <param name="deliveryTime">投递的时间</param>
        /// <param name="shardingKey">分片Key</param>
        public abstract void SendMessage(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "");

        /// <summary>
        /// 发送单向消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="tag">标签</param>
        /// <param name="deliveryTime">投递时间</param>
        /// <param name="shardingKey">分区key</param>
        public abstract void SendMessageByOneway(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "");
    }
}