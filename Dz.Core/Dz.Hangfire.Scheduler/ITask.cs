using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Hangfire.Scheduler
{
    public interface ITask
    {
        string Queue { get; }
        string Cron { get; }

        void Perform();
    }
}
