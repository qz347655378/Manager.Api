using Microsoft.AspNetCore.Builder;

namespace API.Core.Exception
{
    public static class ExceptionHandleExtensions
    {
        public static void UseExceptionHandle(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
