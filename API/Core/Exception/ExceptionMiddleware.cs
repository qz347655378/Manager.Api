using API.ViewModel;
using Common.Enum;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Exception
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (System.Exception e)
            {
                await ExceptionHandleAsync(context, e);
            }
        }

        private static async Task ExceptionHandleAsync(HttpContext context, System.Exception exception)
        {
            Log.Error(exception, exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { code = ResponseStatusEnum.BadRequest, msg = exception.Message }));
        }
    }
}
