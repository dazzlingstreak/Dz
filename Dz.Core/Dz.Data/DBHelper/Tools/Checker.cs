using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Data
{
    public static class Checker
    {
        /// <summary> 如果condition是true 则抛出异常
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="message">异常消息</param>
        public static void IsTrue(bool condition, string message)
        {
            if (condition)
            {
                throw new Exception(message);
            }
        }

        /// <summary> 如果condition是false 则抛出ExceptionCode.ParameterError异常
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="message">异常消息</param>
        public static void IsFalse(bool condition, string message)
        {
            if (condition == false)
            {
                throw new Exception(message);
            }
        }
    }
}
