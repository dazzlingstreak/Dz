using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class NormalParameter : ParameterBase
    {
        IFormatterConverter _converter;

        public NormalParameter(string name, Type type) : base(name, type)
        {
            _converter = Components.GetConverter(type, true);
        }

        protected override void Try(string arg, out object value)
        {
            try
            {
                value = _converter.Convert(arg, ParameterType);
            }
            catch
            {
                value = null;
            }
        }

        protected override void Try(RequestValues args, out object value)
        {
            value = null;
        }
    }
}
