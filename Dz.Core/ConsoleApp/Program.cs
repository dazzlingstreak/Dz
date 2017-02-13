using Dz.OSS;
using Dz.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            //OssHelper.DeleteObject("测试文件.jpg");

            //OssHelper.GetObject("测试文件.jpg", @"D:\DwProjects\hh.jpg");
        }
    }
}
