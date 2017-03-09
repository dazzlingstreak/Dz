using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    /// <summary>
    /// WebApi的方法
    /// </summary>
    public class ApiAction
    {
        /// <summary>
        /// 方法元数据
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public ParameterInfo[] Parameters { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="method"></param>
        public ApiAction(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method", "method不能为空");
            }
            else if (!method.IsPublic)
            {
                throw new ArgumentException("method必须是公共的(public)", "method");
            }
            else
            {
                Method = method;
                Parameters = method.GetParameters();
            }
        }

        /// <summary>
        /// 执行action
        /// </summary>
        /// <param name="api"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Execute(IWebApi api)
        {
            return Method.Invoke(api, Parameters);
        }
    }
}
