using IBLL;
using IBLL.System;
using Models.System;

namespace BLL.System
{
    public class MenuActionBll : BaseBll<MenuAction>, IMenuActionBll
    {
        public MenuActionBll(IBaseBll<MenuAction> currentDal) : base(currentDal)
        {
        }
    }
}
