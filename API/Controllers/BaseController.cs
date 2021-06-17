using API.Core.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.System;
using System;

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
                return JwtHelper.GetUserInfo(HttpContext);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }



}
