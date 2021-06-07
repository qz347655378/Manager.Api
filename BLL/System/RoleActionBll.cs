using IBLL;
using IBLL.System;
using Models.System;

namespace BLL.System
{
    public class RoleActionBll : BaseBll<RoleAction>, IRoleActonBll
    {
        public RoleActionBll(IBaseBll<RoleAction> currentDal) : base(currentDal)
        {
        }
    }
}
