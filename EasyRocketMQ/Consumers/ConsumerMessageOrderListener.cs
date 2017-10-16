using ons;
using System;

namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 顺序消息侦听器
    /// </summary>
    public class ConsumerMessageOrderListener : MessageOrderListener
    {
        /// <summary>
        /// 消费消息的方法
        /// </summary>
        private readonly Func<Message, ConsumeOrderContext, OrderAction> consumeFunc;

        public ConsumerMessageOrderListener(Func<Message, ConsumeOrderContext, OrderAction> consumeFunc)
        {
            this.consumeFunc = consumeFunc;
        }

        public override OrderAction consume(Message message, ConsumeOrderContext context)
        {
            return consumeFunc.Invoke(message, context);
        }
    }
}