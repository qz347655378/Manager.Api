using Models.System;
using System.Threading.Tasks;

namespace IBLL.System
{
    public interface IUserInfoBll : IBaseBll<UserInfo>
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserInfo> LoginAsync(string account, string password);
    }
}
