using API.Core;
using API.ViewModel;
using API.ViewModel.Login;
using Common.Enum;
using Common.Secure;
using IBLL.System;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/sys/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserInfoBll _userInfoBll;
        public LoginController(IUserInfoBll userInfoBll)
        {
            _userInfoBll = userInfoBll;
        }
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
                var userInfo = await _userInfoBll.LoginAsync(model.UserName, EncryptHelper.Hash256Encrypt(model.Password));
                if (userInfo != null)
                {
                    var claims = new[]
                    {
                        new Claim(nameof(UserInfo.Id),userInfo.Id.ToString()),
                        new Claim(nameof(UserInfo.Mobile),userInfo.Mobile),
                        new Claim(nameof(UserInfo.Nickname),userInfo.Nickname),
                        new Claim(nameof(UserInfo.UserName),userInfo.Account)
                    };
                    result.Msg = "登录成功";
                    result.Code = ResponseStatusEnum.Ok;
                    result.Data = new LoginResponseModel
                    {
                        Expired = JwtHelper.GetTimeStamp(DateTime.Now.AddDays(30)),
                        Token = JwtHelper.BuildJwtToken(claims)
                    };
                }
                else
                {
                    result.Msg = "用户名或密码错误";
                }

            }
            catch (Exception e)
            {
                result.Code = ResponseStatusEnum.InternalServerError;
                result.Msg = e.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCaptcha")]
        public ResponseResult<string> GetCaptcha()
        {
            var result = new ResponseResult<string>
            {
                Msg = "获取失败"
            };
            try
            {
                char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
                var rnd = new Random();
                var code = string.Empty;
                //生成验证码字符串 
                for (var i = 0; i < 4; i++)
                {
                    code += character[rnd.Next(character.Length)];
                }

                result.Code = ResponseStatusEnum.Ok;
                result.Msg = "获取成功";
                result.Data = code.ToLower();
                Log.Information("获取验证码");
            }
            catch (Exception e)
            {
                result.Code = ResponseStatusEnum.BadRequest;
                result.Msg = e.Message;
                Log.Error(e, e.Message);
            }



            return result;
        }

    }
}
