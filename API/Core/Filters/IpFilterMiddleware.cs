using API.Core.LogExtensions;
using API.ViewModel;
using Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.Filters
{
    /// <summary>
    /// ip过滤
    /// </summary>
    public class IpFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public IpFilterMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await IpFilter(context);
            await _next.Invoke(context);
        }

        /// <summary>
        /// ip过滤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IpFilter(HttpContext context)
        {

            //  var s = _configuration.GetSection("IpWhitelist").Get<List<string>>();
            //ip白名单
            var whitelist = _configuration.GetSection("IpWhitelist").Get<List<string>>();


            //ip黑名单
            var blacklist = _configuration.GetSection("IpBlacklist").Get<List<string>>();


            //当前IP
            var currentIp = context.GetClientIp();
            var result = JsonConvert.SerializeObject(new ResponseResult<string> { Msg = "当前IP禁止访问任何资源", Code = ResponseStatusEnum.BadRequest });

            if (blacklist.Exists(c => c.Equals(currentIp)))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(result);
            }

            if (whitelist.Count > 0 && whitelist.IndexOf("*") != -1 && whitelist.IndexOf(currentIp) != -1)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(result);
            }

        }
    }
}
