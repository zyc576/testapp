using Microsoft.Extensions.Configuration;
using System.IO;

namespace ZTunnel.Pmms.Core
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppSettingHelper
    {
        #region Properties

        /// <summary>
        /// 类唯一入口
        /// </summary>
        public static readonly AppSettingHelper Instance = new AppSettingHelper();

        private IConfigurationRoot Config { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private AppSettingHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Config = builder.Build();
        }

        /// <summary>
        /// 获取配置内容
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>返回配置内容</returns>
        public string GetConfig(string name)
        {
            return this.Config.GetSection(name).Value;
        }

        #endregion
    }
}
