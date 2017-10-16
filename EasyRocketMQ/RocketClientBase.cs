using ons;
using System;

namespace EasyRocketMQ
{
    /// <summary>
    /// rocketmq 客户端
    /// </summary>
    public abstract class RocketClientBase
    {
        /// <summary>
        /// 访问服务器的KeyId
        /// </summary>
        protected string AccessKeyId { get; private set; }

        /// <summary>
        /// 访问服务器的密钥
        /// </summary>
        protected string AccessKeySecret { get; private set; }

        /// <summary>
        /// 客户端工厂属性
        /// </summary>
        protected ONSFactoryProperty FactoryProperty { get; private set; }

        private string logPath = AppDomain.CurrentDomain.BaseDirectory + @"\log";

        /// <summary>
        /// 日志保存路径
        /// </summary>
        protected string LogPath
        {
            get
            {
                return logPath;
            }
            set
            {
                logPath = value;
                FactoryProperty.setFactoryProperty(ONSFactoryProperty.LogPath, logPath);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accessKeyId">访问服务器的KeyId</param>
        /// <param name="accessKeySecret">访问服务器的密钥</param>
        protected RocketClientBase(string accessKeyId, string accessKeySecret)
        {
            this.AccessKeyId = accessKeyId;
            this.AccessKeySecret = accessKeySecret;

            this.FactoryProperty = this.CreateDefaultFactoryProperty();
        }

        /// <summary>
        /// 创建默认的客户端工厂属性
        /// </summary>
        /// <returns></returns>
        private ONSFactoryProperty CreateDefaultFactoryProperty()
        {
            ONSFactoryProperty factoryProperty = new ONSFactoryProperty();
            factoryProperty.setFactoryProperty(ONSFactoryProperty.AccessKey, AccessKeyId);
            factoryProperty.setFactoryProperty(ONSFactoryProperty.SecretKey, AccessKeySecret);
            factoryProperty.setFactoryProperty(ONSFactoryProperty.LogPath, this.LogPath);

            return factoryProperty;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// 关闭
        /// </summary>
        public abstract void Shutdown();
    }
}