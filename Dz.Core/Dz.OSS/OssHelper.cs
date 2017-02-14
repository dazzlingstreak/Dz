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
        private static string _preFolder => "wow/" + DateTime.Now.ToString("yyyyMM") + "/";

        /// <summary>
        /// Oss存储空间名称
        /// </summary>
        private static string _bucketName => ConfigurationManager.AppSettings["BucketName"];

        #region 一般上传

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
            key = _preFolder + key;
            OssClientProvider.GetOssClient(config).PutObject(bucketName, key, content);
            return key;
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
            key = _preFolder + key;
            OssClientProvider.GetOssClient(config).PutObject(bucketName, key, content, new ObjectMetadata() { ContentMd5 = md5 });
            return key;
        }

        /// <summary>
        /// 异步上传指定的文件到指定的OSS的存储空间
        /// </summary>
        /// <param name="key">文件的在OSS上保存的名称</param>
        /// <param name="content">文件流</param>
        /// <param name="bucketName">指定的存储空间名称</param>
        /// <param name="config">OSS地址的配置信息</param>
        /// <returns></returns>
        public static string AsyncPutObject(string key, Stream content, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            key = _preFolder + key;
            OssClientProvider.GetOssClient(config).BeginPutObject(bucketName, key, content, null, new object());
            return key;
        }

        #endregion

        #region 分片上传

        static int partSize = 50 * 1024 * 1024;

        /// <summary>
        /// 分片上传
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="bucketName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string UploadMultipart(string key, Stream content, string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            key = _preFolder + key;
            var client = OssClientProvider.GetOssClient(config);
            //初始化Multipart Upload
            var request = new InitiateMultipartUploadRequest(bucketName, key);//指定上传文件的名字和所属存储空间
            var result = client.InitiateMultipartUpload(request);//返回结果中含有UploadId ，它是区分分片上传事件的唯一标识

            //Upload Part本地上传
            var fileSize = content.Length;
            var partCount = fileSize / partSize;
            if (fileSize % partSize != 0)
            {
                partCount++;
            }
            var partETags = new List<PartETag>();
            for (var i = 0; i < partCount; i++)
            {
                var skipBytes = (long)partSize * i;
                content.Seek(skipBytes, 0);
                var size = (partSize < fileSize - skipBytes) ? partSize : fileSize - partSize;//每片上传的文件大小，最后一片大小处理
                var partRequest = new UploadPartRequest(bucketName, key, result.UploadId) { InputStream = content, PartSize = size, PartNumber = i + 1 };
                var partResult = client.UploadPart(partRequest);
                partETags.Add(partResult.PartETag);
            }

            //完成分片上传：OSS收到用户提交的Part列表后，会逐一验证每个数据Part的有效性，当所有的数据Part验证通过后，OSS会将这些part组合成一个完整的文件
            var completeMultipartUploadRequest =
                new CompleteMultipartUploadRequest(bucketName, key, result.UploadId);
            foreach (var partETag in partETags)
            {
                completeMultipartUploadRequest.PartETags.Add(partETag);
            }
            client.CompleteMultipartUpload(completeMultipartUploadRequest);

            return key;
        }

        #endregion

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
            var obj = OssClientProvider.GetOssClient(config).GetObject(bucketName, key);
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
            OssClientProvider.GetOssClient(config).DeleteObject(bucketName, key);
        }

        /// <summary>
        /// 获取Referer白名单，可以设置referer，用于防盗链
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string[] GetBucketReferer(string bucketName = "", OssConfig config = null)
        {
            if (bucketName == "") { bucketName = _bucketName; }
            var client = OssClientProvider.GetOssClient(config);
            var rc = client.GetBucketReferer(bucketName);
            return rc.RefererList.Referers;
        }

    }
}
