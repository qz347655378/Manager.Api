using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Models.System;

namespace API.Core.JWT
{
    public static class JwtHelper
    {

        /// <summary>
        /// 创建JWTToken
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string BuildJwtToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.GetConfigToString("JWTSetting:JWTKey")));
            var expiresAt = DateTime.Now.AddDays(ConfigHelper.GetConfigToInt("JWTSetting:JWTExpires"));
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = ConfigHelper.GetConfigToString("JWTSetting:JWTIssuer"),
                Audience = ConfigHelper.GetConfigToString("JWTSetting.Audience"),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// 获取时间戳  13位
        /// </summary>
        /// <param name="dtime">The dtime.</param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dtime)
        {
            var ts = dtime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
            if (string.IsNullOrEmpty(token))
            {
                return new UserInfo();
            }
            var claims = new JwtSecurityToken(token).Claims;
            var enumerable = claims.ToList();
            return new UserInfo
            {
                Id = Convert.ToInt32(enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Id))?.Value ?? ""),
                Mobile = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Mobile))?.Value ?? "",
                Nickname = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Nickname))?.Value ?? "",
                Account = enumerable.FirstOrDefault(c => c.Type == nameof(Models.System.UserInfo.Account))?.Value ?? ""
            };
        }
    }
}
