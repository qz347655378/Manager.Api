using Common.Enum;
using Common.Secure;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table("UserInfo")]
    public class UserInfo : Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; } = UserType.Ordinary;

        /// <summary>
        /// 密码 默认密码123456
        /// </summary>
        public string Password { get; set; } = EncryptHelper.Hash256Encrypt("123456");
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 最后一次登录时的IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public EnableEnum AccountStatus { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleInfo RoleInfo { get; set; }

    }
}
