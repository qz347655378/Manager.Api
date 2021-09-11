using System;
using Common.Enum;
using Common.Secure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("UserInfo")]
    [Index("Account", IsUnique = true)]
    public class UserInfo : Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; } = UserType.Ordinary;

        /// <summary>
        /// 密码 默认密码123456
        /// </summary>
        [Required, MaxLength(256)]
        public string Password { get; set; } = EncryptHelper.Hash256Encrypt("123456");
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string Mobile { get; set; }
        /// <summary>
        /// 最后一次登录时的IP
        /// </summary>
        [MaxLength(50)]
        public string Ip { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public EnableEnum AccountStatus { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }


        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual RoleInfo RoleInfo { get; set; }

    }
}
