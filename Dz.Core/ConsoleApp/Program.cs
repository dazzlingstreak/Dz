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
            #region Redis

            //RedisProvider.StringSet("Dz.Redis", "ah yo22", TimeSpan.FromMinutes(1));
            //Console.WriteLine(RedisProvider.StringGet("Dz.Redis"));
            //Console.ReadLine();

            #endregion

            #region OSS

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

            #endregion

            #region delegate/event

            var radiant = new Radiant();
            new Radiant.BPEventHandler((string s) => { }).Invoke("");//委托可以在外部单独调用，实例化

            radiant.Ban_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingBanEnd); //解说员订阅天辉的Ban事件，天辉的Ban事件的类型是BPEventHandler的，它可以将与自己定义一样入参形式和返参形式的方法注册到事件上，这样事件发布时，那么事件上注册的各方法依次执行。表现为：发布事件->订阅方响应事件
            radiant.Ban_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingBanAnalysis);
            radiant.Ban_Event += new Radiant.BPEventHandler(Player.AfterBanEnd);//扩展一个选手的订阅，和解说员一样，可以注册自己的方法至事件上。设计模式：观察者模式
            radiant.Ban_Event += new Radiant.BPEventHandler(Player.AnalysisBanEnd);

            radiant.Ban("德鲁伊");
            Console.ReadLine();

            #endregion
        }
    }
}
