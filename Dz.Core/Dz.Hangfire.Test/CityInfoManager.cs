using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Data;

namespace Dz.Hangfire.Test
{
    public class CityInfoManager
    {
        public static async Task UpdateCityPinYin()
        {
            string strSql = @"update city set pinyin2={0} where ID=1";
            using (var db = DataBaseContext.GetDataBase("top_LoanForWrite"))
            {
                await db.Sql(strSql, DateTime.Now.ToString("yyyyMMdd hh:mm:ss")).ExecuteNonQuery();
            }
        }
    }
}
