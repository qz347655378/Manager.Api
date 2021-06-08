using IBLL.System;
using IDAL;
using Models.System;

namespace BLL.System
{
    public class RoleInfoBll : BaseBll<RoleInfo>, IRoleInfoBLl
    {
        public RoleInfoBll(IBaseDal<RoleInfo> currentDal) : base(currentDal)
        {
        }
    }
}
