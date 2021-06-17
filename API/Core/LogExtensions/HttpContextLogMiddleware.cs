using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;
using API.Core.JWT;

namespace API.Core.LogExtensions
{
    public class HttpContextLogMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpContextLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpContextLogAsync(context);
            await _next(context);
        }

        private static void HttpContextLogAsync(HttpContext context)
        {
            LogContext.PushProperty("ClientIp", context.GetClientIp());
            LogContext.PushProperty("API", context.Request.Path);
            LogContext.PushProperty("RequestMethod", context.Request.Method);
            LogContext.PushProperty("ResponseStatus", context.Response.StatusCode);
            LogContext.PushProperty("Account", JwtHelper.GetUserInfo(context).Account);
        }
    }



}
