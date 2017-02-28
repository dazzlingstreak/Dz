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
            var dire = new Dire();
            new Radiant.BPEventHandler((string s) => { }).Invoke("");//委托可以在外部单独调用，实例化

            radiant.Ban_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingBanEnd); //解说员订阅天辉的Ban事件，天辉的Ban事件的类型是BPEventHandler的，它可以将与自己定义一样入参形式和返参形式的方法注册到事件上，这样事件发布时，那么事件上注册的各方法依次执行。表现为：发布事件->订阅方响应事件
            radiant.Ban_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingBanAnalysis);
            radiant.Ban_Event += new Radiant.BPEventHandler(Player.AfterBanEnd);//扩展一个选手的订阅，和解说员一样，可以注册自己的方法至事件上。设计模式：观察者模式
            radiant.Ban_Event += new Radiant.BPEventHandler(Player.AnalysisBanEnd);

            //注：使用事件的好处，而不是使用常规的IF判断 ：if(ban){ 解说员干什么，选手干什么}，用了if,那么天辉的Ban方法就会很复杂，如果又有了现场观众需要干什么，网络直播需要干什么，就要不停的扩展修改这个方法。
            //使用事件的话，Ban方法不用修改，只需要将其他的情况注册到事件下，就可以了

            //radiant.Pick_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingPickEnd);
            //radiant.Pick_Event += new Radiant.BPEventHandler(LiveCommentating.CommentatingPickAnalysis);

            //dire.Ban_Event += new Dire.BPEventHandler(LiveCommentating.CommentatingBanEnd);
            //dire.Ban_Event += new Dire.BPEventHandler(LiveCommentating.CommentatingBanAnalysis);
            //dire.Pick_Event += new Dire.BPEventHandler(LiveCommentating.CommentatingPickEnd);
            //dire.Pick_Event += new Dire.BPEventHandler(LiveCommentating.CommentatingPickAnalysis);

            radiant.Ban("德鲁伊");
            //dire.Ban("小强");
            //dire.Ban("小精灵");
            //radiant.Ban("卡尔");
            //radiant.Pick("大鱼人");
            //dire.Pick("半人马酋长");
            //dire.Pick("术士");
            //radiant.Pick("小狗");

            Console.ReadLine();

            #endregion
        }
    }
}
