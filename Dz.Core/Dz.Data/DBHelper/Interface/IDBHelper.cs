using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Data
{
    public interface IDBHelper : IDisposable
    {
        #region config

        /// <summary>
        /// 配置文件中的链接名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 定义的连接字符串值
        /// </summary>
        string ConnectString { get; set; }

        /// <summary>
        /// 数据提供程序的名称
        /// </summary>
        string ProviderName { get; set; }

        #endregion

        #region transaction

        /// <summary>
        /// 开启默认事务
        /// </summary>
        /// <returns></returns>
        Task Begin();

        /// <summary>
        /// 提交默认事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚默认事务
        /// </summary>
        void Rollback();

        #endregion
    }
}
