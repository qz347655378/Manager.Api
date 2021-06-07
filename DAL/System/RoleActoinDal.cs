using IDAL.System;
using Models;
using Models.System;

namespace DAL.System
{
    public class RoleActoinDal : BaseDal<RoleAction>, IRoleActionDal
    {
        public RoleActoinDal(ManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
