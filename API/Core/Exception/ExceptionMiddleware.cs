using API.ViewModel;
using Common.Enum;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Threading.Tasks;

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
                await ExceptionHandleAsync(context, e).ConfigureAwait(false);
            }
        }

        private static Task ExceptionHandleAsync(HttpContext context, System.Exception exception)
        {
            var result = new ResponseResult<string>
            {
                Msg = exception.Message,
                Code = ResponseStatusEnum.InternalServerError
            };
            context.Response.ContentType = "application/json";
            Log.Error(exception,exception.Message);
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
