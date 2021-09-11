using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Filters;
using API.ViewModel;
using Common.Enum;
using IBLL.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Models.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.System
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserInfoBll _userInfoBll;
        public UserController(IUserInfoBll userInfoBll)
        {
            _userInfoBll = userInfoBll;
        }

        /// <summary>
        /// 获取所有未删除用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName">用户名称</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet(nameof(GetUserInfo)),Action("User.GetUserInfo")]
        public async Task<ResponseResult<TableData<UserInfo>>> GetUserInfo(int page,int limit,string userName,int roleId)
        {
            var result = new ResponseResult<TableData<UserInfo>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = ""
            };

            try
            {

                if (string.IsNullOrEmpty(userName))
                {
                    userName = "";
                }

                var list = await _userInfoBll.GetPageListAsync(page, limit, out var totalCount,
                    c => c.IsDelete == 0 && (c.Account.Contains(userName) || c.RoleId == roleId), c => c.Id, true);

                var list2 = await _userInfoBll.GetList(c => c.IsDelete == 0 && (c.Account.Contains(userName) || c.RoleId == roleId)).Include(c=>c.RoleInfo).OrderBy(c => c.Id).Skip((page - 1) * page).Take(limit).ToListAsync();
             

                result.Data = new TableData<UserInfo>
                {
                    CurrentPage = page,
                    TotalCount = totalCount,
                    List = list2
                };
            }
            catch (Exception e)
            {
                result.Code = ResponseStatusEnum.BadRequest;
                result.Msg = $"服务器错误，{e.Message}";
            }

            return result;
        }


    }
}
