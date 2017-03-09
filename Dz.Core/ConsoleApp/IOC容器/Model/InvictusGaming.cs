using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Model
{
    public class InvictusGaming : IClub
    {
        void IClub.Competition()
        {
            Console.WriteLine("IG战队在比赛...");
        }

        void IClub.PlayerIntroduction()
        {
            Console.WriteLine("我们的Team是：430");
        }

        void IClub.Register()
        {
            Console.WriteLine("我们是IG战队");
        }

        void IClub.Train()
        {
            Console.WriteLine("IG战队在训练...");
        }
    }
}
