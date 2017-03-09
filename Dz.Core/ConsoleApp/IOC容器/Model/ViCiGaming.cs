using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Model
{
    public class ViCiGaming : IClub
    {
        void IClub.Competition()
        {
            Console.WriteLine("VG战队在比赛...");
        }

        void IClub.PlayerIntroduction()
        {
            Console.WriteLine("我们的Team是：Buring，Yang，End，Rotk，Fy");
        }

        void IClub.Register()
        {
            Console.WriteLine("我们是VG战队");
        }

        void IClub.Train()
        {
            Console.WriteLine("VG战队在训练...");
        }
    }
}
