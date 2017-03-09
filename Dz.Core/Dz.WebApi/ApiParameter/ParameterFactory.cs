using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class ParameterFactory
    {
        /// <summary>
        /// 根据参数元数据创建API的请求参数类型
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ParameterBase CreateApiParameter(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter null exception");
            }
            return CreateApiParameter(parameter.Name, parameter.ParameterType);
        }

        /// <summary>
        /// 根据参数元数据的属性：Name，Type创建API的请求参数类型
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static ParameterBase CreateApiParameter(string name, Type type)
        {
            ParameterBase parameter;

            if (Nullable.GetUnderlyingType(type) != null)
            {
                parameter = new NullableParameter(name, type);
            }
            else
            {
                parameter = new NormalParameter(name, type);
            }

            return parameter;
        }
    }
}
