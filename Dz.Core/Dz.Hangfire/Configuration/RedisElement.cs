using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Dz.Hangfire.Configuration
{
    public class RedisElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionstring", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this["connectionstring"]; }
            set { this["connectionstring"] = value; }
        }
    }
}