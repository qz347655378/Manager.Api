using Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table(nameof(RoleInfo))]
    public class RoleInfo : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public EnableEnum RoleStatus { get; set; } = EnableEnum.Disable;
    }
}
