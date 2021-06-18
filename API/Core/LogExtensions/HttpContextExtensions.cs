using Microsoft.AspNetCore.Http;

namespace API.Core.LogExtensions
{
    /// <summary>
    /// HttpContext拓展类
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取客户端请求IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return ip;
        }
    }
}
