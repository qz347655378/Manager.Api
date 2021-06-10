using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;

namespace API.Core
{
    public class HttpContextEnricher:ILogEventEnricher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Action<LogEvent, ILogEventPropertyFactory, HttpContext> _enrichAction;
        public HttpContextEnricher(IServiceProvider serviceProvider) : this(serviceProvider, null)
        { }
        public HttpContextEnricher(IServiceProvider serviceProvider, Action<LogEvent, ILogEventPropertyFactory, HttpContext> enrichAction)
        {
            _serviceProvider = serviceProvider;
            if (enrichAction == null)
            {
                _enrichAction = (logEvent, propertyFactory, httpContext) =>
                {
                    var x_forwarded_for = new StringValues();
                    if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    {
                        x_forwarded_for = httpContext.Request.Headers["X-Forwarded-For"];
                    }

                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ClientIp",
                        JsonConvert.SerializeObject(x_forwarded_for)));
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("API",
                        httpContext.Request.Path));
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestMethod",
                        httpContext.Request.Method));
                    if (httpContext.Response.HasStarted)
                    {
                        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ResponseStatus",
                            httpContext.Response.StatusCode));
                    }
                };
            }
            else
            {
                _enrichAction = enrichAction;
            }
        }


        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            //var httpContext = _serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
            var httpContext = _serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
            if (null != httpContext)
            {
                _enrichAction.Invoke(logEvent, propertyFactory, httpContext);
            }
        }

    }
}
