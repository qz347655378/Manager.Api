using IDAL.System;
using Models;
using Models.System;

namespace DAL.System
{
    public class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal
    {
        public UserInfoDal(ManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
