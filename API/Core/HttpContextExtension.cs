using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Core
{
    /// <summary>
    /// HttpContext拓展类
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// 获取客户端请求IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpContext context)
        {
            var ip = context.Request.Headers["x-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            return ip;
        }
    }
}
