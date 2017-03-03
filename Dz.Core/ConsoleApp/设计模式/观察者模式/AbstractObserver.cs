using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Observer
{
    abstract class AbstractObserver
    {
        public abstract void Update();
    }

    /// <summary>
    /// 具体观察者
    /// </summary>
    class ConcreteObserver : AbstractObserver
    {
        public string Name { get; set; }

        public override void Update()
        {
            Console.WriteLine($"我是观察者{Name}，我收到更新的通知了......");
        }
    }
}
