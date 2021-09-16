using API.Core.JWT;
using API.ViewModel;
using Common.Enum;
using IBLL.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Models.System;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Cache;
using Common.Secure;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        //   private readonly IMemoryCache _memoryCache;
        private readonly IRoleActonBll _roleActonBll;

        public ActionFilter(/*IMemoryCache memoryCache, */IRoleActonBll roleActonBll)
        {
            //  _memoryCache = memoryCache;
            _roleActonBll = roleActonBll;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.ContentType = "";
            //参数校验
            if (!context.ModelState.IsValid)
            {
                var errorMsg = context.ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage)
                    .FirstOrDefault();
                context.Result = new BadRequestObjectResult(new
                {
                    Code = ResponseStatusEnum.BadRequest,
                    Msg = errorMsg,
                    Data = ""
                });
                return;
            }


            //用户授权
            var desc = (ControllerActionDescriptor)context.ActionDescriptor;
            var action = desc.MethodInfo.GetCustomAttributes(typeof(ActionAttribute), false).FirstOrDefault() as ActionAttribute;
            if (action == null) return;
            var userInfo = JwtHelper.GetUserInfo(context.HttpContext);

            if (userInfo == null)
            {
                var result = new ResponseResult<string>
                {
                    Msg = "请先登录",
                    Code = ResponseStatusEnum.Unauthorized
                };

                context.Result = new UnauthorizedObjectResult(result);
                return;
            }

            // var actionList = _memoryCache.Get<List<MenuAction>>(userInfo.Account);
            var key = EncryptHelper.Hash256Encrypt(userInfo.Account);

            var actionList = MemoryCacheHelper.Cache.Get<List<MenuAction>>(key);
            if (actionList == null)
            {
                actionList = _roleActonBll.GetRoleAction(userInfo.RoleId, userInfo.UserType == UserType.Administrator).Result;
                //将用户权限放进缓存中，缓存默认30分钟动态过期
                // _memoryCache.Set(userInfo.Account, actionList);
                MemoryCacheHelper.Cache.Set(key, actionList);
            }
            if (actionList.Find(c => c.Code.Equals(action.Code)) == null && userInfo.UserType != UserType.Administrator)
            {
                context.Result = new ForbidResult();

                Log.Warning($"【{userInfo.Account}】试图访问【{action.Code}】被拒绝，没有权限！");
            }
            else
            {
                //这样每次访问都会被记录在日志里面
                Log.Information($"【{userInfo.Account}】访问【{action.Code}】成功");
            }

        }
    }
}
