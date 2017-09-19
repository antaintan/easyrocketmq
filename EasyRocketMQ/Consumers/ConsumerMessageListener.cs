using ons;
using System;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 消费者消息倾听器
    /// </summary>
    public class ConsumerMessageListener : MessageListener
    {
        /// <summary>
        /// 消费消息的方法
        /// </summary>
        private Func<Message, ConsumeContext, ons.Action> consumeFunc;

        public ConsumerMessageListener(Func<Message, ConsumeContext, ons.Action> consumeFunc)
        {
            this.consumeFunc = consumeFunc;
        }

        public override ons.Action consume(Message message, ConsumeContext context)
        {
            return consumeFunc.Invoke(message, context);
        }
    }
}