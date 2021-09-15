using System;
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
using Common.Language;
using Microsoft.Extensions.Localization;


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
        private readonly IStringLocalizer<Language> _localizer;

        public MenuController(IMenuActionBll menuActionBll, IRoleActonBll roleActonBll, IStringLocalizer<Language> localizer)
        {
            _menuActionBll = menuActionBll;
            _roleActonBll = roleActonBll;
            _localizer = localizer;
        }

        /// <summary>
        /// 获取所有可用菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllMenuList)), Action("Menu.GetAllMenuList")]
        public async Task<ResponseResult<List<MenuAction>>> GetAllMenuList()
        {
            var result = new ResponseResult<List<MenuAction>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "请求成功"
            };

            var list = await _menuActionBll.GetListAsync(
                c => c.IsDelete == DeleteStatus.NoDelete);
            result.Data = list;
            return result;
        }

        /// <summary>
        /// 根据菜单父ID获取菜单列表
        /// </summary>
        /// <param name="menuParentId">菜单父Id</param>
        /// <returns></returns>
        [HttpGet(nameof(GetMenuByParentId)), Action("Menu.Read")]
        public async Task<ResponseResult<List<DtreeViewModel>>> GetMenuByParentId(int menuParentId)
        {
            var result = new ResponseResult<List<DtreeViewModel>>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "",
            };

            var list = await _menuActionBll.GetListAsync(c => c.ParentId == menuParentId && c.IsDelete == DeleteStatus.NoDelete);

            var dtreeList = list.Select(menuAction => new DtreeViewModel { CheckArr = 0, Id = menuAction.Id, ParentId = menuAction.ParentId, Title = menuAction.ActionName }).ToList();
            result.Data = dtreeList;

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

            if (await _menuActionBll.AddAsync(menuAction))
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
        public async Task<IActionResult> MenuEdit([FromBody] MenuAction menuAction)
        {
            menuAction.EditTime = DateTime.Now;
            return await AddOrEditMenuCallback(OperationType.Edit, menuAction);
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


        /// <summary>
        /// 移除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        [HttpGet(nameof(Remove)), Action("Menu.Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var menu = await _menuActionBll.GetListAsync(c => c.Id == id);
            if (!menu.Any()) return NotFound(_localizer["NotFound"]);
            menu[0].IsDelete = DeleteStatus.Delete;
            menu[0].ActionStatus = EnableEnum.Disable;
            menu[0].EditTime = DateTime.Now;
            return await AddOrEditMenuCallback(OperationType.Edit, menu[0]);
        }


        public async Task<IActionResult> SetMenuStatus(int id, EnableEnum status)
        {
            var menu = await _menuActionBll.GetListAsync(c => c.Id == id);
            if (!menu.Any()) return NotFound(_localizer["NotFound"]);
            menu[0].ActionStatus = status;
            menu[0].EditTime = DateTime.Now;
            return await AddOrEditMenuCallback(OperationType.Edit, menu[0]);
        }


        /// <summary>
        /// 编辑获取添加菜单
        /// </summary>
        /// <param name="type"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<IActionResult> AddOrEditMenuCallback(OperationType type, MenuAction model)
        {
            return type switch
            {
                OperationType.Add => await _menuActionBll.AddAsync(model)
                    ? (ActionResult)Ok(_localizer["OK"])
                    : BadRequest(_localizer["BadRequest"]),
                OperationType.Edit => await _menuActionBll.EditAsync(model)
                    ? (ActionResult)Ok(_localizer["OK"])
                    : BadRequest(_localizer["BadRequest"]),
                _ => BadRequest(_localizer["BadRequest"])
            };
        }

    }



}
