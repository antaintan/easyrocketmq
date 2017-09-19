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
        private TransactionProducer producer;

        public TransactionProducerClient(string accessKeyId, string accessKeySecret, string producerId)
            : base(accessKeyId, accessKeySecret, producerId)
        {
            //producer = new TransactionProducer();
        }

        public override void Start()
        {
            producer.start();
        }

        public override void Shutdown()
        {
            producer.shutdown();
        }

        public override void SendMessage(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "")
        {
            var message = new Message(topic, tag, tag + "_" + Guid.NewGuid().ToString("N"), content);

            producer.send(message, new ExtendedLocalTransactionExecuter(msg => {
                // 本地事务执行

                return true;
            }));
        }

        public override void SendMessageByOneway(string topic, string content, string tag = "", DateTime? deliveryTime = null, string shardingKey = "")
        {
            throw new NotSupportedException("事务消息不支持Oneway发送");
        }
    }
}