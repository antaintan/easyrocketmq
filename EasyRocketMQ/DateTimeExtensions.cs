using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRocketMQ
{
    /// <summary>
    /// DateTime扩展类
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 转换成时间戳
        /// </summary>
        /// <param name="dateTime">当前时间</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}