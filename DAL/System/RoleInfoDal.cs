using IDAL.System;
using Models;
using Models.System;

namespace DAL.System
{
    public class RoleInfoDal : BaseDal<RoleInfo>, IRoleInfoDal
    {
        public RoleInfoDal(ManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
