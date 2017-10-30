using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRocketMQ
{
    /// <summary>
    /// 没有找到消息监听器异常类
    /// </summary>
    public class NotFoundMessageListenerException :Exception
    {
        public NotFoundMessageListenerException()
            : base("没有找到消息监听器")
        {

        }
    }
}
