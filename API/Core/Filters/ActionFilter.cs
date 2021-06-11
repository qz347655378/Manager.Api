using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModel;
using Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Models.System;
using Newtonsoft.Json;
using Serilog;

namespace API.Core.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        private readonly IMemoryCache _memoryCache;

        public ActionFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var desc = (ControllerActionDescriptor)context.ActionDescriptor;
            var action = desc.MethodInfo.GetCustomAttributes(typeof(ActionAttribute), false).FirstOrDefault() as ActionAttribute;
            if (action == null) return base.OnActionExecutionAsync(context, next);
            var userInfo = GetUserByJwtToken.GetUserInfo(context.HttpContext);
            var actionList = _memoryCache.Get<List<MenuAction>>(userInfo.Account);
            if (actionList.Find(c => c.Code.Equals(action.Code)) == null)
            {
                var result = new ResponseResult<string>
                {
                    Msg = "没有权限访问该资源",
                    Code = ResponseStatusEnum.Forbidden
                };
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
                Log.Warning($"【{userInfo.Account}】试图访问【{action.Code}】被拒绝，没有权限");

            }
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
