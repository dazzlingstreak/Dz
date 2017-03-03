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

        [Fact]
        public void TestContractImport()
        {
            var contracts = ExcelImport.Import<ContractTest>(@"C:\Users\Administrator\Desktop\合同分城市平均天数.xlsx", 4);
            var groups = contracts.Where(p => p.Cycle.IndexOf("0000-00-00 00:00:00") == -1).GroupBy(p => p.BuildingNo).Select(g => new { No = g.Key, Count = g.Count() });
            var buildingNos = groups.Where(p => p.Count > 1).Select(p => p.No).ToList();
            contracts = contracts.Where(p => buildingNos.Contains(p.BuildingNo)).ToList();

            foreach (var item in contracts)
            {
                var times = item.Cycle.Split('至');
                item.BeginTime = DateTime.Parse(times[0]);
                item.EndTime = DateTime.Parse(times[1]);
            }

            var treatContracts = new List<ContractTest>();
            foreach (var no in buildingNos)
            {
                var list = contracts.Where(p => p.BuildingNo == no).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (!list[i].TimeRangeOnly(list[j]))
                        {
                            if (!treatContracts.Any(p => p.ID == list[i].ID))
                            {
                                treatContracts.Add(list[i]);
                            }
                            treatContracts.Add(list[j]);
                        }
                    }
                }
            }
            var bytes = ExcelExport.Export(treatContracts, @"C:\Users\Administrator\Desktop\合同分城市到处模板.xlsx", "Sheet1", 0);
            using (var fs = new FileStream(@"C:\Users\Administrator\Desktop\合同分城市导出.xlsx", FileMode.OpenOrCreate, FileAccess.Write))
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

    public class ContractTest
    {
        [Column(Title = "城市编号", DefaultValue = "")]
        public string CityNo { get; set; }

        [Column(Title = "城市名称", DefaultValue = "")]
        public string CityName { get; set; }

        [Column(Title = "楼盘编号", DefaultValue = "")]
        public string BuildingNo { get; set; }

        [Column(Title = "楼盘名称", DefaultValue = "")]
        public string BuildingName { get; set; }

        [Column(Title = "周期", DefaultValue = "")]
        public string Cycle { get; set; }

        [Column(Title = "天数", DefaultValue = "")]
        public string Days { get; set; }

        [Column(Title = "标识", DefaultValue = "")]
        public string Identification { get; set; }

        [Column(Title = "序号", DefaultValue = "")]
        public string ID { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool TimeRangeOnly(object obj)
        {
            var temp = (ContractTest)obj;
            return this.EndTime < temp.BeginTime || this.BeginTime > temp.EndTime;
        }
    }

}
