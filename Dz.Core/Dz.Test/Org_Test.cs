using Dz.Core;
using Dz.NPOI;
using Dz.Test.Model.Org;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dz.Test
{
    public class Org_Test
    {
        [Fact]
        public async Task<List<LoanCity>> GetProvince()
        {
            var baseUri = ConfigurationManager.AppSettings["Organization.Domain"];
            var methodPath = ConfigurationManager.AppSettings["GetProvince"];

            var request = new HttpRequestMessage() { };
            request.RequestUri = new Uri(baseUri + methodPath);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Authorization", "b0c1c7be-fbcc-4636-9de5-c516fd512223");
            request.Headers.Add("App-Key", "e3c9c373c82d6d5bd4a4aa6a8269bbe6");
            request.Headers.Add("User-Agent", "Mozilla");
            var provinceResult = await HttpClientHelper.SendAsync<List<Province>>(request);
            var provinces = provinceResult.Data;
            var returnList = new List<LoanCity>();
            provinces.ForEach(p => { returnList.Add(new LoanCity() { ID = p.provinceId, PID = 0, CityName = p.provinceName }); });
            return returnList;
        }

        public async Task<List<LoanCity>> GetProvinceCitys(int provinceID)
        {
            var baseUri = ConfigurationManager.AppSettings["Organization.Domain"];
            var methodPath = ConfigurationManager.AppSettings["GetCitys"];

            var request = new HttpRequestMessage() { };
            request.RequestUri = new Uri(baseUri + methodPath + "?provinceId=" + provinceID);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Authorization", "b0c1c7be-fbcc-4636-9de5-c516fd512223");
            request.Headers.Add("App-Key", "e3c9c373c82d6d5bd4a4aa6a8269bbe6");
            request.Headers.Add("User-Agent", "Mozilla");

            var citysResult = await HttpClientHelper.SendAsync<List<City>>(request);
            var citys = citysResult.Data;
            var returnList = new List<LoanCity>();
            citys.ForEach(p => { returnList.Add(new LoanCity() { ID = p.cityId, PID = provinceID, CityName = p.cityName }); });
            return returnList;
        }

        [Fact]
        public async Task GetAllCitys()
        {
            var cityList = new List<LoanCity>();
            var provinces = await GetProvince();
            cityList.AddRange(provinces);
            foreach (var item in provinces)
            {
                var citys = await GetProvinceCitys(item.ID);
                cityList.AddRange(citys);
            }
            cityList = cityList.OrderBy(p => p.ID).ToList();
            var bytes = ExcelExport.Export(cityList, @"C:\Users\Administrator\Desktop\城市列表导出模板.xlsx", "城市列表", 0, null);
            using (var fs = new FileStream(@"C:\Users\Administrator\Desktop\城市导出.xlsx", FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
