using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Redis
{
    public sealed class RedisProvider
    {
        /// <summary>
        /// 延时加载主-FromConfig
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazyMaster = new Lazy<ConnectionMultiplexer>(() => { return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["MasterRedis"]); });

        /// <summary>
        /// 延时加载从-FromConfig
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazySlave = new Lazy<ConnectionMultiplexer>(() => { return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["SlaveRedis"]); });

        /// <summary>
        /// 主写
        /// </summary>
        public static ConnectionMultiplexer writeConn => lazyMaster.Value;

        /// <summary>
        /// 从读
        /// </summary>
        public static ConnectionMultiplexer readConn => lazySlave.Value;

        /// <summary>
        /// 获取写实例
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private static IDatabase GetWriteDb(ConnectionMultiplexer conn = null)
        {
            if (conn == null)
            {
                return writeConn.GetDatabase();
            }
            else
            {
                return conn.GetDatabase();
            }
        }

        /// <summary>
        /// 获取写实例
        /// </summary>
        /// <param name="db">数据库实例ID</param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private static IDatabase GetWriteDb(int db, ConnectionMultiplexer conn = null)
        {
            if (conn == null)
            {
                return writeConn.GetDatabase(db);
            }
            else
            {
                return conn.GetDatabase(db);
            }
        }

        /// <summary>
        /// 获取读实例
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private static IDatabase GetReadDb(ConnectionMultiplexer conn = null)
        {
            if (conn == null)
            {
                return readConn.GetDatabase();
            }
            else
            {
                return conn.GetDatabase();
            }
        }

        /// <summary>
        /// 获取读实例
        /// </summary>
        /// <param name="db">数据库实例ID</param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private static IDatabase GetReadDb(int db, ConnectionMultiplexer conn = null)
        {
            if (conn == null)
            {
                return readConn.GetDatabase(db);
            }
            else
            {
                return conn.GetDatabase(db);
            }
        }

        #region string

        /// <summary>
        /// 异步设置key为string类型的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(string key, string value, TimeSpan ts, ConnectionMultiplexer conn = null)
        {
            var database = GetWriteDb(conn);
            return await database.StringSetAsync(key, value, ts);
        }

        /// <summary>
        /// 设置key为string类型的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool StringSet(string key, string value, TimeSpan ts, ConnectionMultiplexer conn = null)
        {
            var database = GetWriteDb(conn);
            return database.StringSet(key, value, ts);
        }

        /// <summary>
        /// 异步获取key为string类型的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static async Task<string> StringGetAsync(string key, ConnectionMultiplexer conn = null)
        {
            var database = GetReadDb(conn);
            return await database.StringGetAsync(key);
        }

        /// <summary>
        /// 获取key为string类型的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static string StringGet(string key, ConnectionMultiplexer conn = null)
        {
            var database = GetReadDb(conn);
            return database.StringGet(key);
        }

        #endregion

    }
}
