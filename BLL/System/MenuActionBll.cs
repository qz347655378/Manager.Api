using IBLL.System;
using IDAL;
using Models.System;

namespace BLL.System
{
    public class MenuActionBll : BaseBll<MenuAction>, IMenuActionBll
    {
        public MenuActionBll(IBaseDal<MenuAction> currentDal) : base(currentDal)
        {
        }
    }
}
