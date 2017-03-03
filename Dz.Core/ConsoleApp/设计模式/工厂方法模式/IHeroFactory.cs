using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.FactoryMethod
{
    interface IHeroFactory
    {
        AbstractHero CreateHero();
    }

    class RadiantHeroFactory : IHeroFactory
    {
        public AbstractHero CreateHero()
        {
            return new RadiantHero();
        }
    }

    class DireHeroFactory : IHeroFactory
    {
        public AbstractHero CreateHero()
        {
            return new DireHero();
        }
    }

}
