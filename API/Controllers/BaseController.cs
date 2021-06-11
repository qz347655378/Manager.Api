using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.System;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using API.Core.Filters;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 获取token中的用户信息
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true), Authorize]
        public UserInfo GetUserByJwtToken()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                var claims = new JwtSecurityToken(token).Claims;
                var enumerable = claims.ToList();
                return new UserInfo
                {
                    Id = Convert.ToInt32(enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Id))?.Value ?? ""),
                    Mobile = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Mobile))?.Value ?? "",
                    Nickname = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Nickname))?.Value ?? "",
                    Account = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Account))?.Value ?? ""
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }

   

}
