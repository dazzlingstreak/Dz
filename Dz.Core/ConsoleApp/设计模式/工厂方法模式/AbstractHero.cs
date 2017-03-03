using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.FactoryMethod
{
    public class AbstractHero
    {
        public string HeroName { get; set; }
    }

    public class RadiantHero : AbstractHero
    {
        public override string ToString()
        {
            return $"天辉英雄：{HeroName}";
        }
    }

    public class DireHero : AbstractHero
    {
        public override string ToString()
        {
            return $"夜魇英雄：{HeroName}";
        }
    }
}
