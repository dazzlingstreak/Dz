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
        public IClub _club;

        public string Address { get; set; }

        public DAC(IClub club)
        {
            _club = club;
        }

        public override void InviteClub()
        {
            _club.PlayerIntroduction();
            _club.Competition();
        }
    }
}
