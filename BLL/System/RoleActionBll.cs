using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enum;
using IBLL.System;
using IDAL;
using Microsoft.EntityFrameworkCore.Internal;
using Models.System;

namespace BLL.System
{
    public class RoleActionBll : BaseBll<RoleAction>, IRoleActonBll
    {
        private readonly IMenuActionBll _menuActionBll;
        public RoleActionBll(IBaseDal<RoleAction> currentDal, IMenuActionBll menuActionBll) : base(currentDal)
        {
            _menuActionBll = menuActionBll;
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="isAdmin">是否是管理员</param>
        /// <returns></returns>
        public async Task<List<MenuAction>> GetRoleAction(int roleId, bool isAdmin)
        {
            var roleActionList = GetList(c => c.RoleId == roleId);
            var roleActions = new List<MenuAction>();
            if (isAdmin)
            {
                roleActions = await _menuActionBll.GetListAsync(c => c.IsDelete == DeleteStatus.NoDelete && c.ActionStatus == EnableEnum.Enable);
            }
            else
            {
                foreach (var roleAction in roleActionList)
                {
                    var model = await _menuActionBll.GetListAsync(c => c.Id == roleAction.ActionId && c.IsDelete == DeleteStatus.NoDelete && c.ActionStatus == EnableEnum.Enable);
                    if (model.Any())
                    {
                        roleActions.Add(model[0]);
                    }

                }
            }

            return roleActions;
        }
    }
}
