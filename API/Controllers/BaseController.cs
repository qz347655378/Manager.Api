using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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
                    Id = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Id))?.Value ?? "",
                    Mobile = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Mobile))?.Value ?? "",
                    Nickname = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.Nickname))?.Value ?? "",
                    UserName = enumerable.FirstOrDefault(c => c.Type == nameof(UserInfo.UserName))?.Value ?? ""
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class UserInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 联系号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
    }

}
