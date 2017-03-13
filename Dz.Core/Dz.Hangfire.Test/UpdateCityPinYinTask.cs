using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Hangfire.Test
{
    public class UpdateCityPinYinTask : TaskBase
    {
        public override string Queue => "UpdateCityPinYin";

        public override string Cron => "*/1 * * * *";

        protected async override Task PerformAsync(Guid guid)
        {
            await CityInfoManager.UpdateCityPinYin();
        }
      
    }
}
