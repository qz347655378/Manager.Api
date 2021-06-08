using IBLL.System;
using IDAL;
using Models.System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.System
{
    public class UserInfoBll : BaseBll<UserInfo>, IUserInfoBll
    {
        public UserInfoBll(IBaseDal<UserInfo> currentDal) : base(currentDal)
        {
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserInfo> LoginAsync(string account, string password)
        {
            var list = await GetListAsync(c => c.Account == account && c.Password == password);
            return list.Any() ? list.FirstOrDefault() : null;
        }
    }
}
