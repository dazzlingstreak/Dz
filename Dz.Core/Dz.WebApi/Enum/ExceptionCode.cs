using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public enum ExceptionCode
    {
        /// <summary> 
        /// 无异常
        /// </summary>
        [Description("无异常")]
        None = 0,
        /// <summary> 调试
        /// </summary>
        [Description("调试")]
        Debug = 1,
        /// <summary> 表示一个可以直接展示给用户的错误信息
        /// </summary>
        [Description("服务器忙,请稍候再试|{0}")]
        CanShowMessage = 2,
        /// <summary> 表示一个HTML文档
        /// </summary>
        [Description("")]
        HTML = 3,
        /// <summary> 表示一个文件
        /// </summary>
        [Description("")]
        File = 4,
        /// <summary> 
        /// 重定向
        /// </summary>
        [Description("")]
        Redirect = 302,
        /// <summary>
        /// API不存在
        /// </summary>
        [Description("API不存在")]
        APINotFound = 404,
        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        SystemError = 500,

        /// <summary> 
        /// 请求太频繁
        /// </summary>
        [Description("请求太频繁")]
        TooManyRequests = 429,
        /// <summary> 
        /// 未知
        /// </summary>
        [Description("未知异常")]
        Unknown = 501,

        /*系统异常*/
        /// <summary>
        /// 数据库操作异常
        /// </summary>
        [Description("数据库操作异常")]
        DataBaseError = 1002,
        /// <summary>
        /// 数据库访问超时
        /// </summary>
        [Description("数据库访问超时")]
        DataBaseTimeout = 1003,
        /// <summary>
        /// 网络异常
        /// </summary>
        [Description("网络异常")]
        NetworkError = 1004,
        /// <summary> 
        /// 外部服务器异常
        /// <para>外部服务器异常|外部服务器异常\n地址{0}\n消息{1}</para>
        /// </summary>
        [Description("外部服务器异常|外部服务器异常\n地址{0}\n消息{1}")]
        OuterServiceError = 1005,
        /// <summary> 外部服务器超时
        /// <para>外部服务器地址{0}</para>
        /// </summary>
        [Description("外部服务器超时|外部服务器超时,地址{0}")]
        OuterServiceTimeout = 1006,
        /// <summary>
        /// API已废弃
        /// </summary>
        [Description("API已废弃|API已废弃:{0}")]
        APIObsolete = 1007,
        /// <summary>
        /// 当前操作未实现
        /// </summary>
        [Description("当前操作未实现")]
        NotImplemented = 1008,
        /// <summary>
        /// 不支持Jsonp请求
        /// </summary>
        [Description("不支持Jsonp请求")]
        NotSupportJsonp = 1009,
        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        IllegalRequest = 1010,
        /// <summary>
        /// 无效操作
        /// </summary>
        [Description("无效操作")]
        InvalidOperation = 1011,

        /*数据异常*/
        /// <summary>
        /// 数据错误
        /// </summary>
        [Description("数据错误")]
        DataError = 2000,
        /// <summary>
        /// 数据不存在
        /// <para>{0} 不存在或被删除</para>
        /// </summary>
        [Description("数据不存在|{0} 不存在或被删除")]
        DataNotFound = 2002,
        /// <summary>
        /// 数据格式错误
        /// <para>数据格式错误,应为({0})格式</para>
        /// </summary>
        [Description("数据格式错误|数据格式错误,应为({0})格式")]
        DataFormatError = 2003,
        /// <summary>
        /// 数据类型错误
        /// <para>数据类型错误,应为({0})类型</para>
        /// </summary>
        [Description("数据类型错误|数据类型错误,应为({0})类型")]
        DataTypeError = 2004,
        /// <summary>
        /// 数据重复
        /// <para>{0}不能重复</para>
        /// </summary>
        [Description("数据重复|{0}已经存在")]
        DataRepeat = 2005,
        /// <summary>
        /// 数据没有授权
        /// </summary>
        [Description("数据没有授权")]
        DataUnsupported = 2006,
        /// <summary>
        /// 数据无效
        /// <para>数据无效:{0}</para>
        /// </summary>
        [Description("数据无效|数据无效:{0}")]
        DataInvalid = 2007,

        /*参数异常*/
        /// <summary>
        /// 参数错误
        /// <para>参数错误,参数名:{0}</para>
        /// </summary>
        [Description("参数错误|参数错误,参数名:{0}")]
        ParameterError = 3000,
        /// <summary>
        /// 缺少参数
        /// <para>缺少参数:{0}</para>
        /// </summary>
        [Description("缺少参数|缺少参数:{0}")]
        ParameterMissing = 3002,
        /// <summary>
        /// 参数不能为空值
        /// <para>参数:{0} 值不能为空</para>
        /// </summary>
        [Description("参数不能为空值|参数:{0} 值不能为空")]
        ParameterRequire = 3003,
        /// <summary>
        /// 参数格式错误
        /// <para>参数:{0} 格式错误,应为({1})格式</para>
        /// </summary>
        [Description("参数格式错误|参数:{0} 格式错误,应为({1})格式")]
        ParameterFormatError = 3004,
        /// <summary>
        /// 参数值超出允许范围
        /// <para>参数:{0} 的值只能为{1}</para>
        /// </summary>
        [Description("参数值超出允许范围|参数:{0} 的值只能为{1}")]
        ParameterOutOfRange = 3005,
        /// <summary>
        /// 令牌无效
        /// </summary>
        [Description("令牌无效")]
        TokenRejected = 3006,
        /// <summary>
        /// 令牌已使用
        /// </summary>
        [Description("令牌已使用")]
        TokenUsed = 3007,
        /// <summary>
        /// 令牌过期
        /// </summary>
        [Description("令牌过期")]
        TokenExpired = 3008,
        /// <summary>
        /// 签名无效
        /// </summary>
        [Description("签名无效")]
        SignatureInvalid = 3009,
        /// <summary> 
        /// 时间戳超出允许范围
        /// </summary>
        [Description("时间戳超出允许范围")]
        TimestampOutOfRange = 3010,
        /// <summary> 
        /// 存在非法字符
        /// <para>存在非法字符({0})</para>
        /// </summary>
        [Description("存在非法字符|存在非法字符({0})")]
        HasInvalidCharacters = 3011,
        /// <summary> 
        /// 参数值长度超过限制
        /// <para>参数:{0} 长度只能为{1}</para>
        /// </summary>
        [Description("参数值长度超过限制|参数:{0} 长度只能为{1}")]
        ParameterLengthLimit = 3012,
        /// <summary> 
        /// 存在有敏感词
        /// <para>存在敏感词({0})</para>
        /// </summary>
        [Description("存在有敏感词|存在敏感词({0})")]
        HasForbidWorld = 3013,


        /*权限异常*/
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoAuthority = 4000,
        /// <summary>
        /// 未登录
        /// </summary>
        [Description("未登录")]
        NotLogin = 4002,
        /// <summary>
        /// IP限制
        /// </summary>
        [Description("IP限制")]
        IPLimit = 4003,
        /// <summary>
        /// API未授权
        /// </summary>
        [Description("API未授权")]
        APIUnsupported = 4004,
    }
}
