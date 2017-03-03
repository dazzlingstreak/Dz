using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Visitor
{
    /// <summary>
    /// 地点的抽象类
    /// </summary>
    abstract class AbstractSpot
    {
        /// <summary>
        /// 接受访问者
        /// </summary>
        /// <param name="visitor"></param>
        public abstract void Accept(AbstractVisitor visitor);
    }

    class GreatWall : AbstractSpot
    {
        public override void Accept(AbstractVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Museum : AbstractSpot
    {
        /// <summary>
        /// 接受访问者
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(AbstractVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

}
