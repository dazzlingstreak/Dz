using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Visitor
{
    /// <summary>
    /// 游客的抽象类
    /// </summary>
    abstract class AbstractVisitor
    {
        /// <summary>
        /// 参观长城
        /// </summary>
        /// <param name="spot"></param>
        public abstract void Visit(GreatWall spot);

        /// <summary>
        /// 参观博物馆
        /// </summary>
        /// <param name="spot"></param>
        public abstract void Visit(Museum spot);

    }

    class ChineseVisitor : AbstractVisitor
    {
        public override void Visit(GreatWall spot)
        {
            Console.WriteLine($"中国人游览长城{spot.ToString()}");
        }

        public override void Visit(Museum spot)
        {
            Console.WriteLine($"中国人游览博物馆{spot.ToString()}");
        }
    }

    class FranceVisitor : AbstractVisitor
    {
        public override void Visit(GreatWall spot)
        {
            Console.WriteLine($"法国人游览长城{spot.ToString()}");
        }

        public override void Visit(Museum spot)
        {
            Console.WriteLine($"法国人游览博物馆{spot.ToString()}");
        }

    }
}
