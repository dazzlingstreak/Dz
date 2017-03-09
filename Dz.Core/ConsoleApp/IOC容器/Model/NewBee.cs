using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Model
{
    public class NewBee : IClub
    {
        void IClub.Competition()
        {
            Console.WriteLine("NewBee战队在比赛...");
        }

        void IClub.PlayerIntroduction()
        {
            Console.WriteLine("我们的Team是：Kaka，Sccc，Kpii，UUU9，Faith");
        }

        void IClub.Register()
        {
            Console.WriteLine("我们是NewBee战队");
        }

        void IClub.Train()
        {
            Console.WriteLine("NewBee战队在训练...");
        }
    }
}
