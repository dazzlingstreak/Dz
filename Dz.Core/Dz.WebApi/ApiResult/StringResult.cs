using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class StringResult : IApiResult
    {
        private string _str;

        public StringResult()
        {
        }

        public StringResult(string data)
        {
            _str = data;
        }

        /// <summary>
        /// 获取Http响应信息
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        HttpResponseMessage IApiResult.GetResponseMessage(DzApiControllerBase api)
        {
            var content = GetHttpContent();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = content
            };
        }

        /// <summary>
        /// 获取Http内容
        /// </summary>
        /// <returns></returns>
        private HttpContent GetHttpContent()
        {
            var str = this.ToString();
            return new StringContent(str, Encoding.UTF8, "text/plain");
        }

        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _str ?? string.Empty;
        }
    }
}
