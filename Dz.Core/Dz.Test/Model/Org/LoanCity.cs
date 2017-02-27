using Dz.NPOI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Test.Model.Org
{
    public class LoanCity
    {
        [Column(Title = "城市ID", DefaultValue = 0)]
        public int ID { get; set; }
        [Column(Title = "父城市ID", DefaultValue = 0)]
        public int PID { get; set; }
        [Column(Title = "城市名称", DefaultValue = "")]
        public string CityName { get; set; }
    }
}
