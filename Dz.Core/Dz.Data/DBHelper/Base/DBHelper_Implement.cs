using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Dz.Data
{
    public abstract partial class DBHelper : IDBHelper
    {
        #region config

        /// <summary>
        /// 配置文件中的链接名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 定义的连接字符串值
        /// </summary>
        public string ConnectString { get; set; }

        /// <summary>
        /// 数据提供程序的名称
        /// </summary>
        public string ProviderName { get; set; }

        #endregion

        /// <summary>
        /// 获取数据库提供程序的工厂实例
        /// </summary>
        protected abstract DbProviderFactory factory { get; }

        #region transaction

        IDbTransaction _tran;

        /// <summary>
        /// 开启默认事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task Begin()
        {
            if (_tran == null)
            {
            }
        }

        /// <summary>
        /// 提交默认事务
        /// </summary>
        public virtual void Commit()
        {
            if (_tran != null)
            {
                _tran.Commit();
                _tran = null;
            }
        }

        /// <summary>
        /// 回滚默认事务
        /// </summary>
        public virtual void Rollback()
        {
            if (_tran != null)
            {
                _tran.Rollback();
                _tran = null;
            }
        }

        #endregion

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
