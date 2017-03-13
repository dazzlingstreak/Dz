using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Dz.Hangfire.Configuration
{
    public class TaskSchedulerSection : ConfigurationSection
    {
        [ConfigurationProperty("redis", IsRequired = true)]
        public RedisElement Redis
        {
            get { return (RedisElement)this["redis"]; }
            set { this["redis"] = value; }
        }

        [ConfigurationProperty("queues", IsRequired = true)]
        public QueueElement Queues
        {
            get { return (QueueElement)this["queues"]; }
            set { this["queues"] = value; }
        }

    }
}