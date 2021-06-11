using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace API.Core.Filters
{
    public static class GetUserByJwtToken
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Models.System.UserInfo GetUserInfo(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 401;
                throw new System.Exception("请携带token请求！");
            }
            var claims = new JwtSecurityToken(token).Claims;
            var enumerable = claims.ToList();
            return new Models.System.UserInfo
            {
                Id = Convert.ToInt32(enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Id))?.Value ?? ""),
                Mobile = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Mobile))?.Value ?? "",
                Nickname = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Nickname))?.Value ?? "",
                Account = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Account))?.Value ?? ""
            };
        }
    }
}
