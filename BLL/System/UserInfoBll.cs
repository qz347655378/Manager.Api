using IBLL;
using IBLL.System;
using Models.System;

namespace BLL.System
{
    public class UserInfoBll : BaseBll<UserInfo>, IUserInfoBll
    {
        public UserInfoBll(IBaseBll<UserInfo> currentDal) : base(currentDal)
        {
        }
    }
}
