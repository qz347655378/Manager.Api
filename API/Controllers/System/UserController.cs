using API.Core.Filters;
using API.ViewModel;
using Common.Enum;
using Common.I18n;
using Common.Secure;
using IBLL.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.System
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserInfoBll _userInfoBll;
        private readonly IStringLocalizer<Language> _localizer;
        public UserController(IUserInfoBll userInfoBll, IStringLocalizer<Language> localizer)
        {
            _userInfoBll = userInfoBll;
            _localizer = localizer;
        }

        /// <summary>
        /// 获取所有未删除用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName">用户名称</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet(nameof(GetUserInfo)), Action("User.Read")]
        public async Task<ResponseResult<TableData<UserInfo>>> GetUserInfo(int page, int limit, string userName, int roleId)
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

                var list2 = _userInfoBll.GetList(c => c.IsDelete == 0 && (c.Account.Contains(userName) || c.RoleId == roleId)).Include(c => c.RoleInfo);
                var list = await list2.OrderBy(c => c.Id).Skip((page - 1) * page).Take(limit).ToListAsync();
                result.Data = new TableData<UserInfo>
                {
                    CurrentPage = page,
                    TotalCount = list2.Count(),
                    List = list
                };
            }
            catch (Exception e)
            {
                result.Code = ResponseStatusEnum.BadRequest;
                result.Msg = $"{_localizer["InternalServerError"].Value}，{e.Message}";
            }

            return result;
        }

        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetUserById)), Action("User.Read")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userInfoBll.GetListAsync(c => c.Id == id);
            if (!user.Any()) return NotFound(_localizer["NotFound"].Value);
            return Ok(user[0]);
        }

        /// <summary>
        /// 添加普通用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Add)), Action("User.Add")]
        public async Task<IActionResult> Add([FromBody] UserInfo model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest(_localizer["PasswordRequiredError"].Value);
                }

                model.Password = EncryptHelper.Hash256Encrypt(model.Password);

                return await _userInfoBll.AddAsync(model)
                    ? (IActionResult)Ok(_localizer["OK"].Value)
                    : BadRequest(_localizer["BadRequest"].Value);
            }
            catch (DbUpdateException e)
            {
                return BadRequest(new { msg = _localizer["UniqueAccount"].Value, error = e });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Edit)), Action("User.Edit")]
        public async Task<IActionResult> Edit([FromBody] UserInfo model)
        {
            try
            {

                var noChange = new List<string>
                {
                    nameof(model.AccountStatus),
                    nameof(model.Ip),
                    nameof(model.CreateTime),
                    nameof(model.LastLoginTime),
                    nameof(model.UserType),
                    nameof(model.IsDelete),
                };
                if (!string.IsNullOrEmpty(model.Password))
                {
                    model.Password = EncryptHelper.Hash256Encrypt(model.Password);
                }
                else
                {
                    noChange.Add(nameof(model.Password));
                }





                return await _userInfoBll.EditAsync(model, noChange) ? (ActionResult)Ok(_localizer["OK"].Value) : BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(nameof(Remove)), Action("User.Remove")]
        public async Task<ActionResult> Remove(int id)
        {
            var user = _userInfoBll.GetList(c => c.Id == id).FirstOrDefault();
            if (user == null) return new NotFoundObjectResult(new { msg = _localizer["NotFound"].Value });
            user.IsDelete = DeleteStatus.Delete;
            user.AccountStatus = EnableEnum.Disable;
            user.EditTime = DateTime.Now;

            return await _userInfoBll.EditAsync(user) ? (ActionResult)Ok(_localizer["OK"].Value) : BadRequest(_localizer["BadRequest"].Value);
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        [HttpGet(nameof(SetUserStatus)), Action("User.Edit")]
        public async Task<ActionResult> SetUserStatus(int id, EnableEnum accountStatus)
        {
            var user = await _userInfoBll.GetListAsync(c => c.Id == id);
            if (!user.Any()) return new NotFoundObjectResult(_localizer["NotFound"].Value);
            user[0].AccountStatus = accountStatus;
            user[0].EditTime = DateTime.Now;
            if (await _userInfoBll.EditAsync(user[0])) return BadRequest();
            return new OkObjectResult(_localizer["OK"].Value);
        }

    }
}
