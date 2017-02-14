using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Core.Random
{
    public static class RNGCryptoServiceProviderExtensions
    {
        private static byte[] rb = new byte[4];

        /// <summary>
        /// 产生一个非负数的随机数
        /// </summary>
        /// <param name="rngp"></param>
        /// <returns></returns>
        public static int Next(this RNGCryptoServiceProvider rngp)
        {
            rngp.GetBytes(rb);
            var value = BitConverter.ToInt32(rb, 0);
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// 产生一个非负数且最大值 max 以下的随机数
        /// </summary>
        /// <param name="rngp"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(this RNGCryptoServiceProvider rngp, int max)
        {
            return Next(rngp) % (max + 1);
        }

        /// <summary>
        /// 产生一个非负数且最小值在 min 以上最大值在 max 以下的随机数
        /// </summary>
        /// <param name="rngp"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(this RNGCryptoServiceProvider rngp, int min, int max)
        {
            return Next(rngp, max - min) + min;
        }

    }
}
