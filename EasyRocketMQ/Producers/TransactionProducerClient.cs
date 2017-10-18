using ons;
using System;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 事务消息客户端
    /// </summary>
    public class TransactionProducerClient : ProducerClientBase
    {
        /// <summary>
        /// 消息生产者
        /// </summary>
        private readonly TransactionProducer producer;

        public TransactionProducerClient(string accessKeyId, string accessKeySecret, string producerId, Func<Message, TransactionStatus> checkFunc)
            : base(accessKeyId, accessKeySecret, producerId)
        {
            producer = ONSFactory.getInstance().createTransactionProducer(this.FactoryProperty, new ExtendedLocalTransactionChecker(checkFunc));
        }

        public override void Start()
        {
            producer.start();
        }

        public override void Shutdown()
        {
            producer.shutdown();
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="bizFunc">业务方法</param>
        /// <param name="tag">消息标签</param>
        /// <param name="key">消息Key</param>
        /// <returns></returns>
        public string SendMessage(string topic, string content, Func<Message, bool> bizFunc, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);

            var result = producer.send(message, new ExtendedLocalTransactionExecuter(bizFunc));

            return result.getMessageId();
        }

        /// <summary>
        /// 发送定时消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="content">内容</param>
        /// <param name="bizFunc">业务方法</param>
        /// <param name="deliveryTime">指定发送时间</param>
        /// <param name="tag">消息标签</param>
        /// <param name="key">消息Key</param>
        /// <returns></returns>
        public string SendTimingMessage(string topic, string content, Func<Message, bool> bizFunc, DateTime deliveryTime, string tag = "", string key = "")
        {
            var message = ComposeMessage(topic, content, tag, key);
            message.setStartDeliverTime(deliveryTime.ToTimestamp());            

            var result = producer.send(message, new ExtendedLocalTransactionExecuter(bizFunc));

            return result.getMessageId();
        }
    }
}