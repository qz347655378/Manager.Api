using Common.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table(nameof(RoleInfo))]
    [Index(nameof(RoleName), IsUnique = true)]
    public class RoleInfo : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public EnableEnum RoleStatus { get; set; } = EnableEnum.Disable;
    }
}
