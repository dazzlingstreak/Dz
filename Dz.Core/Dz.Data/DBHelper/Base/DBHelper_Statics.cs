using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Data
{
    public partial class DBHelper
    {
        /// <summary>
        ///  创建并返回 IDBHelper
        /// </summary>
        /// <param name="settings">数据连接设置项</param>
        /// <returns></returns>
        public static IDBHelper Create(ConnectionStringSettings settings)
        {
            Checker.IsTrue(settings == null, "参数丢失");
            Checker.IsTrue(string.IsNullOrEmpty(settings.Name), "Name不能为空");
            Checker.IsTrue(string.IsNullOrEmpty(settings.ConnectionString), "ConnectionString不能为空");
            Checker.IsTrue(string.IsNullOrEmpty(settings.ProviderName), "ProviderName不能为空");
            var dbHelper = Create(settings.ProviderName);
            dbHelper.Name = settings.Name;
            dbHelper.ConnectString = settings.ConnectionString;
            dbHelper.ProviderName = settings.ProviderName;
            return dbHelper;
        }

        /// <summary>
        /// 根据数据提供程序名称实例DBHelper
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        private static IDBHelper Create(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new Exception("未声明数据提供程序的名称");
            }
            switch (providerName)
            {
                case "mysql":
                case "mysqlclient":
                case "mysql.data.mysqlclient":
                    return new MySqlHelper();
                case "sqlserver":
                case "mssql":
                case "sqlclient":
                case "system.data.sqlclient":
                    return new SqlServerHelper();
                default:
                    break;
            }

            return Activator.CreateInstance(GetType(providerName)) as IDBHelper;
        }

        /// <summary>
        /// 根据类型名称获取相应类型
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static Type GetType(string typeFullName)
        {
            var type = Type.GetType(typeFullName);
            if (type != null)
            {
                return type;
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in assemblies)
            {
                type = item.GetType(typeFullName);
                if (type != null)
                {
                    return type;
                }
            }
            throw new TypeLoadException("没有找到 " + typeFullName + " 类型!请使用[命名空间.类名]的完整名称");
        }
    }
}
