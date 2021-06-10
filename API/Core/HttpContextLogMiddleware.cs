using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace API.Core
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
            var serviceProvider = context.RequestServices;
            // 将我们自定义的Enricher添加到LogContext中。
            // LogContext功能很强大，可以动态添加属性，具体使用介绍，参见官方wiki文档
            using (LogContext.Push(new HttpContextEnricher(serviceProvider)))
            {
                await _next(context);
            }
        }


    }



}
