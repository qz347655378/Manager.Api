using API.Core;
using API.ViewModel;
using API.ViewModel.Login;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/sys/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回token及token过期时间</returns>
        [HttpPost("login")]
        public async Task<ResponseResult<LoginResponseModel>> LoginAsync([FromBody] LoginRequestModel model)
        {
            var result = new ResponseResult<LoginResponseModel>();

            try
            {
                if (model.UserName == "admin" && model.Password == "admin" && model.Captcha == "123456")
                {
                    var claims = new Claim[]
                    {
                        new Claim(nameof(UserInfo.Id),"1"),
                        new Claim(nameof(UserInfo.Mobile),"17608430013"),
                        new Claim(nameof(UserInfo.Nickname),"超级管理员"),
                        new Claim(nameof(UserInfo.UserName),model.UserName)
                    };
                    result.Msg = "登录成功";
                    result.Code = ResponseStatusEnum.Ok;
                    result.Data = new LoginResponseModel
                    {
                        Expired = JwtHelper.GetTimeStamp(DateTime.Now.AddDays(30)),
                        Token = JwtHelper.BuildJwtToken(claims)
                    };
                }
            }
            catch (Exception e)
            {
                result.Data = null;
                result.Code = ResponseStatusEnum.InternalServerError;
                result.Msg = e.Message;
            }

            return result;
        }
    }
}
