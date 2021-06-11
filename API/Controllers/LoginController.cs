using API.Core.JWT;
using API.ViewModel;
using API.ViewModel.Login;
using Common.Enum;
using Common.Secure;
using IBLL.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models.System;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Core.LogExtensions;

namespace API.Controllers
{
    [Route("api/sys/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserInfoBll _userInfoBll;
        private readonly IMemoryCache _memoryCache;
        // private readonly ILogger _logger;
        public LoginController(IUserInfoBll userInfoBll, IMemoryCache memoryCache/*, ILogger logger*/)
        {
            _userInfoBll = userInfoBll;
            _memoryCache = memoryCache;
            // _logger = logger;
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
                var capchat = _memoryCache.Get<string>(model.CaptchaKey);
                //校验验证码
                if (string.IsNullOrEmpty(capchat) || capchat != model.Captcha.ToLower())
                {
                    result.Msg = "验证码错误或已经过期";
                    Log.Warning($"{model.UserName}登录，验证码错误或失效，登录失败");
                    return result;
                }
                //移除验证码
                _memoryCache.Remove(model.CaptchaKey);
                var userInfo = await _userInfoBll.LoginAsync(model.UserName, EncryptHelper.Hash256Encrypt(model.Password));
                if (userInfo != null)
                {
                    //账号禁用
                    if (userInfo.AccountStatus == EnableEnum.Disable)
                    {
                        result.Msg = "账号已被禁用";
                        // Log.Warning($"{model.UserName}登录，账号已被禁用，登录失败", nameof(LoginAsync), ip);
                        return result;
                    }
                    var claims = new[]
                    {
                        new Claim(nameof(UserInfo.Id),userInfo.Id.ToString()),
                        new Claim(nameof(UserInfo.Mobile),userInfo.Mobile),
                        new Claim(nameof(UserInfo.Nickname),userInfo.Nickname),
                        new Claim(nameof(UserInfo.Account),userInfo.Account),
                        new Claim(nameof(UserInfo.UserType),userInfo.UserType.ToString())
                    };
                    result.Msg = "登录成功";
                    result.Code = ResponseStatusEnum.Ok;
                    result.Data = new LoginResponseModel
                    {
                        Expired = JwtHelper.GetTimeStamp(DateTime.Now.AddDays(30)),
                        Token = JwtHelper.BuildJwtToken(claims)
                    };
                    userInfo.Ip = HttpContext.GetClientIp();
                    userInfo.EditTime = DateTime.Now;
                    await _userInfoBll.EditAsync(userInfo);
                    Log.Information($"{model.UserName}登录成功");
                }
                else
                {
                    Log.Warning($"{model.UserName}登录失败，用户名或密码错误");
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
        public ResponseResult<CaptchaViewModel> GetCaptcha()
        {
            var result = new ResponseResult<CaptchaViewModel>
            {
                Msg = "获取失败"
            };
            try
            {
                var code = CreateCaptcha();
                result.Code = ResponseStatusEnum.Ok;
                result.Msg = "获取成功";
                var key = Guid.NewGuid().ToString();
                result.Data = new CaptchaViewModel
                {
                    CaptchaKey = key,
                    Captcha = code
                };
                //将验证码放进缓存中
                _memoryCache.Set(key, code.ToLower());
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

        /// <summary>
        /// 测试异常
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestException")]
        public ResponseResult<string> TestException()
        {
            throw new Exception("测试异常");
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        private string CreateCaptcha()
        {
            char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            var rnd = new Random();
            var code = string.Empty;
            //生成验证码字符串 
            for (var i = 0; i < 5; i++)
            {
                code += character[rnd.Next(character.Length)];
            }

            return code;
        }
    }
}
