using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //注：使用事件的好处，而不是使用常规的IF判断 ：if(ban){ 解说员干什么，选手干什么}，用了if,那么天辉的Ban方法就会很复杂，如果又有了现场观众需要干什么，网络直播需要干什么，就要不停的扩展修改这个方法。
    //使用事件的话，Ban方法不用修改，只需要将其他的情况注册到事件下，就可以了
    /// <summary>
    /// Dota2 -天辉
    /// </summary>
    class Radiant
    {
        internal delegate void BPEventHandler(string hero);
        internal event BPEventHandler Ban_Event;
        internal event BPEventHandler Pick_Event;

        public void Ban(string hero)
        {
            Console.WriteLine($"天辉Ban {hero} ...");
            Ban_Event?.Invoke(hero);
        }

        public void Pick(string hero)
        {
            Console.WriteLine($"天辉Pick {hero} ...");
            Pick_Event?.Invoke(hero);
        }
    }

    /// <summary>
    /// Dota2 - 夜魇
    /// </summary>
    class Dire
    {
        internal delegate void BPEventHandler(string hero);
        internal event BPEventHandler Ban_Event;
        internal event BPEventHandler Pick_Event;

        public void Ban(string hero)
        {
            Console.WriteLine($"夜魇Ban {hero} ...");
            Ban_Event?.Invoke(hero);
        }

        public void Pick(string hero)
        {
            Console.WriteLine($"夜魇Pick {hero} ...");
            Pick_Event?.Invoke(hero);
        }
    }

    /// <summary>
    /// 现场解说
    /// </summary>
    class LiveCommentating
    {
        public static void CommentatingBanEnd(string hero)
        {
            Console.WriteLine($"解说员说：Ban了{hero},这个英雄...");
        }

        public static void CommentatingBanAnalysis(string hero)
        {
            Console.WriteLine($"解说员说：Ban了{hero},分析下当前局势...");
        }

        public static void CommentatingPickEnd(string hero)
        {
            Console.WriteLine($"解说员说：Pick了{hero},这个英雄...");
        }

        public static void CommentatingPickAnalysis(string hero)
        {
            Console.WriteLine($"解说员说：Pick了{hero}，分析下当前局势...");
        }
    }

    /// <summary>
    /// 比赛选手
    /// </summary>
    public class Player
    {
        public string Name { get; set; }

        public static void AfterBanEnd(string hero)
        {
            Console.WriteLine($"选手想：Ban了{hero},我得想想比赛套路...");
        }

        public static void AnalysisBanEnd(string hero)
        {
            Console.WriteLine($"选手想：Ban了{hero},分析了下目前形势...");
        }

        public static void AfterPickEnd(string hero)
        {
            Console.WriteLine($"选手想：Pick了{hero}，这个英雄谁去使用，走的是哪路...");
        }

        public static void AnalyPickEnd(string hero)
        {
            Console.WriteLine($"选手想：Pick了{hero}，想下这个英雄害怕什么，接下去要不要BAN掉什么...");
        }

    }

}
