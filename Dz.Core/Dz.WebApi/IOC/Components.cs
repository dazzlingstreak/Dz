using blqw.IOC;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public static class Components
    {
        static Components()
        {
            MEF.Import(typeof(Components));
        }

        [Import("GetConverter")]
        public static readonly Func<Type, bool, IFormatterConverter> GetConverter;

    }
}
