using Dz.Test.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dz.Test
{
    public class Common_Test
    {

        [Fact]
        public void Test()
        {
            //var a = JsonConvert.DeserializeObject<object>("{\n\t\"id\":5,\n\t\"std\":{\n\t\t\"Name\":\"张三\",\n\t\t\"Age\":20,\n\t\t\"Sex\":1\n\t}\n}");

            var b = Convert.ChangeType("{\"Name\":\"张三\",\n\t\t\"Age\":20,\n\t\t\"Sex\":1}", typeof(Student));
        }
    }
}
