using Aliyun.OSS;
using Aliyun.OSS.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.OSS
{
    public sealed class OssHelper
    {
        /// <summary>
        /// 上传的文件名的前缀
        /// </summary>
        private static string _preFolder => "wow/" + DateTime.Now.ToString("yyyyMM");

        /// <summary>
        /// Oss存储空间名称
        /// </summary>
        private static string _bucketName => ConfigurationManager.AppSettings["BucketName"];

        /// <summary>
        /// 上传指定的文件到指定的OSS的存储空间
        /// </summary>
        /// <param name="key">文件的在OSS上保存的名称</param>
        /// <param name="content">文件流</param>
        /// <param name="bucketName">指定的存储空间名称</param>
        /// <param name="config">OSS地址的配置信息</param>
        /// <returns></returns>
        public static string PutObject(string key, Stream content, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            OssClientProvider.GetOssClient(config).PutObject(bucketName, _preFolder + key, content);
            return _preFolder + key;
        }

        /// <summary>
        /// 上传指定的文件到指定的OSS的存储空间并且带MD5校验
        /// </summary>
        /// <param name="key">文件的在OSS上保存的名称</param>
        /// <param name="content">文件流</param>
        /// <param name="bucketName">指定的存储空间名称</param>
        /// <param name="config">OSS地址的配置信息</param>
        /// <returns></returns>
        public static string PutObjectMD5(string key, Stream content, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            var md5 = OssUtils.ComputeContentMd5(content, content.Length);
            OssClientProvider.GetOssClient(config).PutObject(bucketName, _preFolder + key, content, new ObjectMetadata() { ContentMd5 = md5 });
            return _preFolder + key;
        }

        /// <summary>
        ///  从指定的OSS存储空间中获取指定的文件
        /// </summary>
        /// <param name="key">文件的在OSS上保存的名称</param>
        /// <param name="fileToDownload">本地存储下载文件的目录</param>
        /// <param name="bucketName">指定的存储空间名称</param>
        /// <param name="config">OSS地址的配置信息</param>
        public static void GetObject(string key, string fileToDownload, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            var obj = OssClientProvider.GetOssClient(config).GetObject(bucketName, _preFolder + key);
            using (var requestStream = obj.Content)
            {
                var buf = new byte[1024];
                using (var fs = File.Open(fileToDownload, FileMode.OpenOrCreate))
                {
                    var len = 0;
                    while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                    {
                        fs.Write(buf, 0, len);
                    }
                }
            }
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="key">待删除的文件名称</param>
        /// <param name="bucketName">文件所在存储空间的名称</param>
        /// <param name="config">OSS地址的配置信息</param>
        public static void DeleteObject(string key, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            OssClientProvider.GetOssClient(config).DeleteObject(bucketName, _preFolder + key);
        }

    }
}
