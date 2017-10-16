using ons;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 消息者基类
    /// </summary>
    public abstract class ConsumerClientBase : RocketClientBase
    {
        /// <summary>
        /// 消费者Id
        /// </summary>
        protected string ConsumerId { get; private set; }

        /// <summary>
        /// 主题
        /// </summary>
        protected string Topic { get; private set; }

        /// <summary>
        /// 子表达式, Tag的过滤，全部使用*, 多个TagA||TagB
        /// </summary>
        protected string SubExpression { get; private set; }

        /// <summary>
        /// 消息者线程数
        /// </summary>
        protected int ConsumerThreadCount { get; set; } = 10;

        protected ConsumerClientBase(string accessKeyId, string accessKeySecret, string topic, string consumerId,
                                    string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret)
        {
            this.ConsumerId = consumerId;
            this.Topic = topic;
            this.SubExpression = subExpression;
            this.ConsumerThreadCount = consumerThreadCount;

            this.FactoryProperty.setFactoryProperty(ONSFactoryProperty.ConsumerId, consumerId);
            this.FactoryProperty.setFactoryProperty(ONSFactoryProperty.ConsumeThreadNums, this.ConsumerThreadCount.ToString());
        }
    }
}