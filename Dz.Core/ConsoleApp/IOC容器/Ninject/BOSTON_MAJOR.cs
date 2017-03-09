using ConsoleApp.IOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Ninject
{
    public class BOSTON_MAJOR : e_sports
    {
        public BOSTON_MAJOR()
        {

        }

        public override void InviteClub(IClub club)
        {
            club.Register();
            club.PlayerIntroduction();
            club.Train();
            club.Competition();
        }
    }
}
