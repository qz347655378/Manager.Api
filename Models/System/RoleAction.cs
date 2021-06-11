using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table(nameof(RoleAction))]
    public class RoleAction 
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
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
