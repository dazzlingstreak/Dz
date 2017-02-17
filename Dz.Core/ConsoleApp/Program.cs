using Dz.OSS;
using Dz.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //RedisProvider.StringSet("Dz.Redis", "ah yo22", TimeSpan.FromMinutes(1));
            //Console.WriteLine(RedisProvider.StringGet("Dz.Redis"));
            //Console.ReadLine();

            //using (var stream = new FileStream(@"D:\DwProjects\23.jpg", FileMode.Open))
            //{
            //    var OssFileName = OssHelper.PutObject("测试文件.jpg", stream);
            //    Console.WriteLine(OssFileName);
            //    Console.ReadLine();
            //}

            //using (var stream = new FileStream(@"D:\DwProjects\23.jpg", FileMode.Open))
            //{
            //    var OssFileName = OssHelper.PutObjectMD5("测试文件3.jpg", stream);
            //    Console.WriteLine(OssFileName);
            //    Console.ReadLine();
            //}

            //using (var stream = new FileStream(@"D:\DwProjects\23.jpg", FileMode.Open))
            //{
            //    var OssFileName = OssHelper.AsyncPutObject("测试文件3.jpg", stream);
            //    Console.WriteLine(OssFileName);
            //    Console.ReadLine();
            //}

            //using (var stream = new FileStream(@"E:\Tools\mysql-5.6.24-win32.1432006610.zip", FileMode.Open))
            //{
            //    var OssFileName = OssHelper.UploadMultipart("mysql.zip", stream);
            //    Console.WriteLine(OssFileName);
            //    Console.ReadLine();
            //}

            //var reffers = OssHelper.GetBucketReferer();

            //OssHelper.DeleteObject("测试文件.jpg");

            //OssHelper.GetObject("测试文件.jpg", @"D:\DwProjects\hh.jpg");
        }
    }
}
