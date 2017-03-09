using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    /// <summary>
    /// API控制器的基类，行为有：执行控制器方法
    /// </summary>
    public class DzApiControllerBase : IWebApi
    {
        /// <summary>
        /// 当前请求的方法
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 当前请求的方式
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        ///当前请求的变量
        /// </summary>
        public RequestValues RequestParameters { get; set; }

        /// <summary>
        /// 当前请求对应的API的方法参数元数据
        /// </summary>
        public ParameterInfo[] Paras { get; set; }

        /// <summary>
        /// 执行当前Api方法
        /// </summary>
        /// <returns></returns>
        protected async Task<object> InvokeAction()
        {
            var methodInfo = this.GetType().GetMethod(ActionName, BindingFlags.Public | BindingFlags.Instance);
            Paras = methodInfo.GetParameters();

            var ApiParameters = new List<ParameterBase>();
            foreach (var item in Paras)
            {
                ApiParameters.Add(ParameterFactory.CreateApiParameter(item));
            }

            var args = new object[ApiParameters.Count];
            for (var i = 0; i < ApiParameters.Count; i++)
            {
                ApiParameters[i].TryParse(RequestParameters, out object obj);
                args[i] = obj;
            }
            return await TryRunTask(methodInfo.Invoke(this, args));
        }

        /// <summary>
        /// 如果返回值是Task则等待Task执行完毕，返回Task.Result,获否则直接返回result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected async Task<object> TryRunTask(object result)
        {
            var task = result as Task;
            if (task == null)
            {
                return result;
            }
            if (task.Status == TaskStatus.Created)
            {
                task.Start();
            }

            await Task.WhenAny(task);

            var type = task.GetType();
            if (type.IsGenericType)
            {
                result = type.GetProperty("Result").GetValue(task);
                if (result is Task)
                {
                    return await TryRunTask(result);
                }
                return result;
            }
            return null;
        }

    }
}
