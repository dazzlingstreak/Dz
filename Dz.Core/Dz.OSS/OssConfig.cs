using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.OSS
{
    public class OssConfig
    {
        /// <summary>
        /// 用于标识用户
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 用户用于加密签名字符串和 OSS 用来验证签名字符串的密钥
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// OSS 访问域名
        /// </summary>
        public string EndPoint { get; set; }
    }
}
