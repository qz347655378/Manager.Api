using IDAL.System;
using Models;
using Models.System;

namespace DAL.System
{
    public class MenuActionDal : BaseDal<MenuAction>, IMenuActioinDal
    {
        public MenuActionDal(ManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
