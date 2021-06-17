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

namespace API.Core.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRoleActonBll _roleActonBll;

        public ActionFilter(IMemoryCache memoryCache, IRoleActonBll roleActonBll)
        {
            _memoryCache = memoryCache;
            _roleActonBll = roleActonBll;
        }

        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //用户授权
            var desc = (ControllerActionDescriptor)context.ActionDescriptor;
            var action = desc.MethodInfo.GetCustomAttributes(typeof(ActionAttribute), false).FirstOrDefault() as ActionAttribute;
            if (action == null) return base.OnActionExecutionAsync(context, next);
            var userInfo = JwtHelper.GetUserInfo(context.HttpContext);
            var actionList = _memoryCache.Get<List<MenuAction>>(userInfo.Account);
            if (actionList == null)
            {
                actionList = _roleActonBll.GetRoleAction(userInfo.RoleId, userInfo.UserType == UserType.Administrator).Result;
                //将用户权限放进缓存中，缓存默认30分钟动态过期
                _memoryCache.Set(userInfo.Account, actionList);
            }
            if (actionList.Find(c => c.Code.Equals(action.Code)) == null && userInfo.UserType != UserType.Administrator)
            {
                var result = new ResponseResult<string>
                {
                    Msg = "没有权限访问该资源",
                    Code = ResponseStatusEnum.Forbidden
                };
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
                Log.Warning($"【{userInfo.Account}】试图访问【{action.Code}】被拒绝，没有权限！");
            }
            else
            {
                //这样每次访问都会被记录在日志里面
                Log.Information($"【{userInfo.Account}】访问【{action.Code}】成功");
            }
            return base.OnActionExecutionAsync(context, next);
        }

    }
}
