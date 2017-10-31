using ons;
using System.Text;

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

        protected ProducerClientBase(string accessKeyId, string accessKeySecret, string producerId)
            : base(accessKeyId, accessKeySecret)
        {
            this.ProducerId = producerId;
            this.FactoryProperty.setFactoryProperty(ONSFactoryProperty.ProducerId, producerId);
        }

        /// <summary>
        /// 组合消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="tag">标签</param>
        /// <param name="key">消息Key</param>
        /// <returns></returns>
        protected Message ComposeMessage(string topic, string content, string tag = "", string key = "")
        {
            var message = new Message(topic, tag, string.Empty);

            var bodyData = Encoding.UTF8.GetBytes(content);
            message.setBody(bodyData, bodyData.Length);

            message.setKey(key);

            return message;
        }
    }
}