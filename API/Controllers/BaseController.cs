﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.System;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using API.Core.JWT;

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
