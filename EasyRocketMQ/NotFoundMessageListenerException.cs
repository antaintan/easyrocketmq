using System;

namespace EasyRocketMQ
{
    /// <summary>
    /// 没有找到消息监听器异常类
    /// </summary>
    public class NotFoundMessageListenerException : Exception
    {
        public NotFoundMessageListenerException()
            : base("没有找到消息监听器")
        {
        }
    }
}