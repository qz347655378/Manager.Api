using API.Core.Filters;
using API.Core.JWT;
using API.ViewModel;
using API.ViewModel.Menu;
using Common.Enum;
using IBLL.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Controllers.System
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuActionBll _menuActionBll;
        private readonly IRoleActonBll _roleActonBll;

        public MenuController(IMenuActionBll menuActionBll, IRoleActonBll roleActonBll)
        {
            _menuActionBll = menuActionBll;
            _roleActonBll = roleActonBll;
        }

        /// <summary>
        /// 获取所有可用菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllMenuList)), Action("Menu.GetAllMenuList")]
        public async Task<ResponseResult<TableData<MenuAction>>> GetAllMenuList(int page, int limit)
        {
            var result = new ResponseResult<TableData<MenuAction>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "请求成功"
            };

            var list = await _menuActionBll.GetPageListAsync(page, limit, out var totalCount,
                c => c.IsDelete == DeleteStatus.NoDelete, c => c.Sort, true);
            result.Data = new TableData<MenuAction>
            {
                TotalCount = totalCount,
                CurrentPage = page,
                List = list
            };
            return result;
        }

        /// <summary>
        /// 根据角色ID获取角色的拥有的菜单权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet(nameof(GetMenuList)), Action("Menu.GetMenuList")]
        public async Task<ResponseResult<List<MenuAction>>> GetMenuList(int roleId)
        {
            var result = new ResponseResult<List<MenuAction>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "请求成功"
            };
            var list = await _roleActonBll.GetRoleAction(roleId, false);
            result.Data = list.OrderBy(c => c.Sort).ToList();
            return result;
        }

        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCurrentUserMenuList)), Action("Menu.GetCurrentUserMenuList")]
        public async Task<ResponseResult<List<MenuAction>>> GetCurrentUserMenuList()
        {
            var result = new ResponseResult<List<MenuAction>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "请求成功"
            };
            var userInfo = JwtHelper.GetUserInfo(HttpContext);
            var list = await _roleActonBll.GetRoleAction(userInfo.RoleId, userInfo.UserType == UserType.Administrator);
            result.Data = list.OrderBy(c => c.Sort).ToList();

            return result;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(MenuAdd)), Action("Menu.MenuAdd")]
        public async Task<ResponseResult<string>> MenuAdd([FromBody] MenuAction menuAction)
        {
            var result = new ResponseResult<string>
            {
                Msg = "添加失败",
                Data = ""
            };

            var menu = await _menuActionBll.AddAsync(menuAction);
            if (menu != null)
            {
                result.Msg = "添加成功";
                result.Code = ResponseStatusEnum.Ok;
            }

            return result;
        }

        /// <summary>
        /// 编辑菜单项
        /// </summary>
        /// <param name="menuAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(MenuEdit)), Action("Menu.MenuEdit")]
        public async Task<ResponseResult<string>> MenuEdit([FromBody] MenuAction menuAction)
        {
            var result = new ResponseResult<string>
            {
                Msg = "修改失败",
                Data = ""
            };

            var menu = await _menuActionBll.EditAsync(menuAction);
            if (menu != null)
            {
                result.Msg = "修改成功";
                result.Code = ResponseStatusEnum.Ok;
            }

            return result;
        }


        /// <summary>
        /// 修改角色菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(EditRoleMenu)), Action("Menu.EditRoleMenu")]
        public async Task<ResponseResult<string>> EditRoleMenu([FromBody] RoleActionViewModel model)
        {
            var result = new ResponseResult<string>
            {
                Msg = "提交失败"
            };
            if (await _roleActonBll.DeleteAsync(c => c.RoleId == model.RoleId))
            {
                foreach (var i in model.ActionIds)
                {
                    var roleAction = new RoleAction
                    {
                        ActionId = i,
                        RoleId = model.RoleId
                    };
                    await _roleActonBll.AddAsync(roleAction);
                }

                result.Msg = "提交成功";
                result.Code = ResponseStatusEnum.Ok;
            }

            return result;
        }

    }
}
