using Microsoft.AspNetCore.Builder;

namespace API.Core.LogExtensions
{
    public static class HttpContextLogExtensions
    {
        // 使用扩展方法形式注入中间件
        public static IApplicationBuilder UseHttpContextLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextLogMiddleware>();
        }
    }
}
