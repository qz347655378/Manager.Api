using API.Core.Filters;
using API.ViewModel;
using API.ViewModel.System;
using Common.Enum;
using Common.I18n;
using Common.Secure;
using IBLL.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Models.System;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.System
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserInfoBll _userInfoBll;
        private readonly IStringLocalizer _localizer;
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
        [HttpGet(nameof(GetUserInfo)), Action("User.Get")]
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
                result.Msg = $"{_localizer["InternalServerError"]}，{e.Message}";
            }

            return result;
        }

        /// <summary>
        /// 添加普通用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Add)), Action("User.Add")]
        public async Task<IActionResult> Add([FromBody] UserInfo model)
        {
            //var result = new ResponseResult<string>();
            //try
            //{
            //    //var user = new UserInfo
            //    //{
            //    //    Account = model.Account,
            //    //    Password = model.Password,
            //    //    Mobile = model.Mobile,
            //    //    Nickname = model.Nickname,  
            //    //    RoleId = model.RoleId
            //    //};

            //    if (await _userInfoBll.AddAsync(model))
            //    {
            //        result.Code = ResponseStatusEnum.Ok;
            //        result.Msg = _localizer["OK"];
            //    }

            //}
            //catch (Exception e)
            //{
            //    result.Code = ResponseStatusEnum.InternalServerError;
            //    result.Msg = e.Message;
            //}
            //return result;

            return await _userInfoBll.AddAsync(model) ? (IActionResult)Ok(_localizer["OK"]) : BadRequest(_localizer["BadRequest"]);


        }


        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Edit)), Action("User.Edit")]
        public async Task<ResponseResult<string>> Edit([FromForm] AddOrEditUserViewModel model)
        {

            var result = new ResponseResult<string>
            {
                Data = ""
            };

            try
            {
                if (model.Id == 0)
                {
                    result.Code = ResponseStatusEnum.Forbidden;
                    result.Msg = $"id{_localizer["NotBeZero"]}";
                }

                var user = await _userInfoBll.GetListAsync(c => c.Id == model.Id);
                if (user.Any())
                {
                    var temp = user[0];
                    temp.Mobile = model.Mobile;
                    temp.Password = string.IsNullOrEmpty(model.Password)
                        ? temp.Password
                        : EncryptHelper.Hash256Encrypt(model.Password);
                    temp.Account = model.Account;
                    temp.Nickname = model.Nickname;
                    temp.RoleId = model.RoleId;
                    temp.EditTime = DateTime.Now;

                    if (await _userInfoBll.EditAsync(temp))
                    {
                        result.Code = ResponseStatusEnum.Ok;
                        result.Msg = _localizer["OK"];

                    }

                }
                else
                {
                    result.Code = ResponseStatusEnum.BadRequest;
                    result.Msg = _localizer["NoDataFound"];
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
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(nameof(Remove)), Action("User.Remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult Remove(int id)
        {
            var user = _userInfoBll.GetList(c => c.Id == id).FirstOrDefault();
            if (user == null) return new NotFoundObjectResult(new { msg = _localizer["NotFound"] });
            user.IsDelete = DeleteStatus.Delete;
            user.AccountStatus = EnableEnum.Disable;
            user.EditTime = DateTime.Now;
            if (_userInfoBll.Edit(user)) return BadRequest(_localizer["InternalServerError"]);
            return Ok(_localizer["OK"]);
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        [HttpGet(nameof(SetUserStatus)), Action("User.SetUserStatus")]
        public async Task<ActionResult> SetUserStatus(int id, EnableEnum accountStatus)
        {
            var user = await _userInfoBll.GetListAsync(c => c.Id == id);
            if (!user.Any()) return new NotFoundObjectResult(_localizer["NotFound"]);
            user[0].AccountStatus = accountStatus;
            user[0].EditTime = DateTime.Now;
            if (await _userInfoBll.EditAsync(user[0])) return BadRequest();
            return new OkObjectResult(_localizer["OK"]);
        }

    }



}
