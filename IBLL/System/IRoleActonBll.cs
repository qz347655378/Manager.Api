using Models.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBLL.System
{
    public interface IRoleActonBll : IBaseBll<RoleAction>
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="isAdmin">是否是管理员</param>
        /// <returns></returns>
        Task<List<MenuAction>> GetRoleAction(int roleId, bool isAdmin);
    }


}
