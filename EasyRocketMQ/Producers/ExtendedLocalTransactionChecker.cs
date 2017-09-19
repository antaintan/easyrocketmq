using ons;
using System;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 扩展本地事务检查者
    /// </summary>
    public class ExtendedLocalTransactionChecker : LocalTransactionChecker
    {
        /// <summary>
        /// 事务检查方法
        /// </summary>
        private Func<Message, TransactionStatus> checkFunc;

        public ExtendedLocalTransactionChecker(Func<Message, TransactionStatus> checkFunc)
        {
            this.checkFunc = checkFunc;
        }

        public override TransactionStatus check(Message msg)
        {
            return checkFunc.Invoke(msg);
        }
    }
}