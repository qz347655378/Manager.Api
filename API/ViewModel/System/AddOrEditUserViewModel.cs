using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel.System
{
    /// <summary>
    /// 用户添加或编辑实体类
    /// </summary>
    public class AddOrEditUserViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
