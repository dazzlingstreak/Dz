using blqw.IOC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class JsonFormBody : NameValueCollection
    {
        private object _data;
        private string _json;

        public JsonFormBody(string json)
        {
            _data = ComponentServices.ToJsonObject(null, json);
            _json = json;
        }

        public override string Get(string name)
        {
            return GetFromData(_data, name)?.ToString();
        }

        protected object GetFromData(object data, string name)
        {
            var map = data as IDictionary;
            if (map != null)
            {
                if (name != null)
                {
                    return map[name];
                }
            }
            return null;
        }
    }
}
