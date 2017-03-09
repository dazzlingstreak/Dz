using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class NullableParameter : ParameterBase
    {
        public NullableParameter(string name, Type type) : base(name, type)
        {
        }

        protected override void Try(string arg, out object value)
        {
            value = null;
        }

        protected override void Try(RequestValues args, out object value)
        {
            value = null;
        }
    }
}
