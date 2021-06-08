using IBLL.System;
using IDAL;
using Models.System;

namespace BLL.System
{
    public class RoleActionBll : BaseBll<RoleAction>, IRoleActonBll
    {
        public RoleActionBll(IBaseDal<RoleAction> currentDal) : base(currentDal)
        {
        }
    }
}
