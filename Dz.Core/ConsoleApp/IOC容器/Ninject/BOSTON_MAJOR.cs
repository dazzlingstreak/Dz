using ConsoleApp.IOC.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Ninject
{
    public class BOSTON_MAJOR : e_sports
    {
        public IClub _club;

        public BOSTON_MAJOR(IClub club)
        {
            _club = club;
        }

        public override void InviteClub()
        {
            _club.Register();
            _club.PlayerIntroduction();
            _club.Train();
            _club.Competition();
        }
    }
}
