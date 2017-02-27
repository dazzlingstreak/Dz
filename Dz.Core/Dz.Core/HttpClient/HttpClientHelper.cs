using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Core
{
    public class HttpClientHelper
    {
        public static async Task<HttpResult<T>> SendAsync<T>(HttpRequestMessage request)
        {
            HttpResult<T> msg = null;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var http = new HttpClient())
                {
                    var response = await http.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        msg = JsonConvert.DeserializeObject<HttpResult<T>>(content);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        msg = new HttpResult<T>() { Code = -1, Message = response.StatusCode.ToString() };
                    }
                }
            }
            catch (Exception ex)
            {
                msg = new HttpResult<T>() { Code = -1, Message = ex.Message };
            }

            stopwatch.Stop();

            Trace.Write($"耗时:{stopwatch.ElapsedMilliseconds}ms 请求参数：{JsonConvert.SerializeObject(request)}  返回结果：{JsonConvert.SerializeObject(msg)};远程接口调用");

            return msg;
        }

        public static async Task<HttpResult<T>> GetAsync<T>(string url)
        {
            HttpResult<T> msg = null;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var http = new HttpClient())
                {
                    var response = await http.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        msg = JsonConvert.DeserializeObject<HttpResult<T>>(content);
                    }
                    else
                    {
                        msg = new HttpResult<T>() { Code = -1, Message = response.StatusCode.ToString() };
                    }
                }
            }
            catch (Exception ex)
            {
                msg = new HttpResult<T>() { Code = -1, Message = ex.Message };
            }

            stopwatch.Stop();

            Trace.Write($"耗时:{stopwatch.ElapsedMilliseconds}ms 请求参数：{url} 返回结果：{JsonConvert.SerializeObject(msg)};远程接口调用");

            return msg;
        }
    }

    public class HttpResult<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
