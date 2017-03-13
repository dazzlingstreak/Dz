using Dz.Core;
using Dz.Hangfire.Test;
using Dz.Test.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        [Fact]
        public async Task TestTopData()
        {
            await CityInfoManager.UpdateCityPinYin();
        }

        [Fact]
        public void TestSendMessage()
        {
            var client = new EmailClient()
            {
                LoginName = ConfigurationManager.AppSettings["EmailFromAddress"],
                Password = ConfigurationManager.AppSettings["EmailFromPassword"],
                SmtpHost = ConfigurationManager.AppSettings["SmtpHost"],
            };

            client.SendMessage(ConfigurationManager.AppSettings["EmailRecipients"], "测试Test", "DAC在即，敬请期待，亚洲盛典");
        }
    }
}
