using Dz.NPOI;
using System;
using System.Collections.Generic;
using System.IO;
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
            var customers = ExcelImport.Import<CustomerTest>(@"C:\Users\Administrator\Desktop\客户导入.xlsx");
            var bytes = ExcelExport.Export(customers, @"C:\Users\Administrator\Desktop\客户导出模板.xlsx", "客户导出", 1, new List<int>() { 6 });
            using (var fs = new FileStream(@"C:\Users\Administrator\Desktop\客户导出.xlsx", FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
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
