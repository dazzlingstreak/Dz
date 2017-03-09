using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dz.WebApi
{
    /// <summary>
    /// 请求变量，包含Form，QueryString，Headers，Cookies
    /// </summary>
    public class RequestValues : UnvalidatedRequestValuesBase
    {
        public override NameValueCollection Form => FormBody ?? _values?.Form;
        public override NameValueCollection QueryString => _values?.QueryString;
        public override NameValueCollection Headers => _values?.Headers;
        public override HttpCookieCollection Cookies => _values?.Cookies;
        public override HttpFileCollectionBase Files => _values?.Files;
        public override string RawUrl => _values?.RawUrl;
        public override string Path => _values?.Path;
        public override string PathInfo => _values?.PathInfo;
        public override Uri Url => _values?.Url;
        /// <summary>
        /// 路由参数
        /// </summary>
        public NameValueCollection RouteDatas { get; set; }
        /// <summary>
        /// 客户端提交的内容字节
        /// </summary>
        public byte[] FormBodyBytes { get; set; }
        /// <summary>
        /// 客户端提交的内容字节转换
        /// </summary>
        public NameValueCollection FormBody { get; set; }

        /// <summary>
        /// 请求变量构造函数
        /// </summary>
        /// <param name="values"></param>
        public RequestValues(UnvalidatedRequestValuesBase values)
        {
            _values = values;
        }

        private UnvalidatedRequestValuesBase _values;

        public override string this[string field] => RouteDatas[field] ?? Form[field] ?? QueryString[field];
    }
}
