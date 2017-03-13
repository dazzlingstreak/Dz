using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dz.Hangfire
{
    internal class TaskConfiguration
    {
        public string ProjectName { get; set; }

        public List<TaskItem> Items { get; set; }
    }

    internal class TaskItem
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}