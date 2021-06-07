using System.Linq;
using System.Threading.Tasks;
using Common.Secure;
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
