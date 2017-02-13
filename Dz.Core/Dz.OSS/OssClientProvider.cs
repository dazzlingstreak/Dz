using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.OSS
{
    public sealed class OssClientProvider
    {
        private static OssConfig _OssConfig => new OssConfig()
        {
            EndPoint = ConfigurationManager.AppSettings["EndPoint"],
            AccessKeyId = ConfigurationManager.AppSettings["AccessKeyId"],
            AccessKeySecret = ConfigurationManager.AppSettings["AccessKeySecret"],
        };

        /// <summary>
        /// 由用户指定的OSS访问地址、阿里云颁发的AccessKeyId/AccessKeySecret构造一个新的OssClient实例。
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static OssClient GetOssClient(OssConfig config = null)
        {
            if (config == null)
            {
                config = _OssConfig;
            }
            return new OssClient(config.EndPoint, config.AccessKeyId, config.AccessKeySecret);
        }

    }
}
