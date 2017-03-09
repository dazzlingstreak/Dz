using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Model
{
    public class Wings : IClub
    {
        void IClub.Competition()
        {
            Console.WriteLine("Wings战队在比赛...");
        }

        void IClub.PlayerIntroduction()
        {
            Console.WriteLine("我们的Team是：IceIce，跳刀跳刀，Faith_Bian，y丶，Shadow");
        }

        void IClub.Register()
        {
            Console.WriteLine("我们是Wings战队");
        }

        void IClub.Train()
        {
            Console.WriteLine("Wings战队在训练...");
        }
    }
}
