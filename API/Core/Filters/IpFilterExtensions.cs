using Microsoft.AspNetCore.Builder;

namespace API.Core.Filters
{
    public static class IpFilterExtensions
    {
        /// <summary>
        /// IP过滤
        /// </summary>
        /// <param name="app"></param>
        public static void UseIpFilter(this IApplicationBuilder app)
        {
            app.UseMiddleware<IpFilterMiddleware>();
        }
    }
}
