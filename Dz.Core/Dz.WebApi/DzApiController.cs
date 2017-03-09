using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Dz.WebApi
{
    public class DzApiController : DzApiControllerBase, IHttpController
    {
        /// <summary>
        /// 请求。
        /// </summary>
        public HttpRequestMessage Request { get; set; }
        /// <summary>
        /// 路由数据。
        /// </summary>
        public IHttpRouteData RouteData { get; set; }
        /// <summary>
        /// 控制器的当前上下文
        /// </summary>
        public HttpControllerContext Context { get; set; }

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="controllerContext"></param>
        protected void Initialize(HttpControllerContext controllerContext)
        {
            Context = controllerContext;
            Request = controllerContext.Request;
            RouteData = controllerContext.RouteData;

            ActionName = RouteData.Values["action"] as string;
            Method = Request.Method;

            var ctx = Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            if (ctx != null)
            {
                RequestParameters = new RequestValues(ctx.Request.Unvalidated);
                ctx.Request.InputStream.Position = 0;
                RequestParameters.FormBodyBytes = ctx.Request.BinaryRead(ctx.Request.ContentLength); //读取body数据
            }
            else
            {
                RequestParameters = new RequestValues(null);
                //TODO:如何取值，可以从Request出发
            }
            var mediaType = Request.Content?.Headers?.ContentType?.MediaType;
            var isJson = mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
            if (isJson)
            {
                var charSet = Request.Content?.Headers?.ContentType?.CharSet;
                var encoding = charSet == null ? Encoding.UTF8 : Encoding.GetEncoding(charSet);
                var jsonString = encoding.GetString(RequestParameters.FormBodyBytes);
                RequestParameters.FormBody = new JsonFormBody(jsonString);
            }

            ReadRouteData(RequestParameters);//获取路由数据
        }

        /// <summary>
        /// 执行用于同步的控制器。
        /// </summary>
        /// <param name="controllerContext">控制器的当前上下文。</param>
        /// <param name="cancellationToken">取消操作的通知。</param>
        /// <returns> 控制器。</returns>
        async Task<HttpResponseMessage> IHttpController.ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            //获取提交信息，执行控制器方法，获取结果返回
            Initialize(controllerContext);
            return ProcessResult(await InvokeAction());
        }

        /// <summary>
        /// 读取路由参数，保存到请求变量中
        /// </summary>
        /// <param name="values">请求变量</param>
        private void ReadRouteData(RequestValues values)
        {
            var nv = new NameValueCollection();
            foreach (var item in RouteData.Values)
            {
                nv.Add(item.Key, item.Value + "");
            }
            values.RouteDatas = nv;
        }

        /// <summary>
        /// 将结果封装为HTTP响应信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private HttpResponseMessage ProcessResult(object result)
        {
            var respose = result as HttpResponseMessage;
            if (respose != null)
            {
                return respose;
            }
            if (result is IApiResult == false)
            {
                result = new JsonResult(result);
            }
            return ((IApiResult)result).GetResponseMessage(this);
        }
    }
}
