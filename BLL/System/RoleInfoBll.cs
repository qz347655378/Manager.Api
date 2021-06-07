using IBLL;
using IBLL.System;
using Models.System;

namespace BLL.System
{
    public class RoleInfoBll : BaseBll<RoleInfo>, IRoleInfoBLl
    {
        public RoleInfoBll(IBaseBll<RoleInfo> currentDal) : base(currentDal)
        {
        }
    }
}
