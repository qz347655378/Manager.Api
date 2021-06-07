using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table(nameof(RoleAction))]
    public class RoleAction : Entity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [ForeignKey(nameof(RoleInfo))]
        public int RoleId { get; set; }

        /// <summary>
        /// 动作ID
        /// </summary>
        [ForeignKey(nameof(MenuAction))]
        public int ActionId { get; set; }

        public virtual RoleInfo RoleInfo { get; set; }
        public virtual MenuAction MenuAction { get; set; }
    }
}
