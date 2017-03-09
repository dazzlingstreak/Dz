using ConsoleApp.IOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Ninject
{
    public class DAC : e_sports
    {
        public override void InviteClub(IClub club)
        {
            club.PlayerIntroduction();
            club.Competition();
        }
    }
}
