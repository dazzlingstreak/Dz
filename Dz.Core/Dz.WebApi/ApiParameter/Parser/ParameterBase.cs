using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public abstract class ParameterBase
    {
        public string Name { get; set; }
        public Type ParameterType { get; set; }

        public ParameterBase(string name, Type type)
        {
            Name = name;
            ParameterType = type;
        }

        public void TryParse(RequestValues values, out object value)
        {
            var paramValue = values[Name];
            if (paramValue != null)
            {
                Try(paramValue, out value);
            }
            else
            {
                Try(values, out value);
            }
        }

        protected abstract void Try(RequestValues args, out object value);

        protected abstract void Try(string arg, out object value);
    }
}
