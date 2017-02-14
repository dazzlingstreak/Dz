using Dz.NPOI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dz.Test
{
    public class NPOI_Test
    {
        [Fact]
        public void TestImport()
        {
            var customers = ExcelImport.Import<CustomerTest>(@"C:\Users\Administrator\Desktop\客户导入.xlsx");
        }

        [Fact]
        public void TestExport()
        {
            ExcelExport.Export(@"C:\Users\Administrator\Desktop\客户导入.xlsx");
        }
    }

    public class CustomerTest
    {
        [Column(Title = "姓名", DefaultValue = "")]
        public string Name { get; set; }

        [Column(Title = "手机号", DefaultValue = "")]
        public string MobilePhone { get; set; }

        [Column(Title = "年龄", DefaultValue = 0)]
        public int Age { get; set; }

        [Column(Title = "住址", DefaultValue = "")]
        public string Address { get; set; }
    }
}
