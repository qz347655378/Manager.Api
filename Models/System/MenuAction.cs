using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    /// <summary>
    /// 菜单实体
    /// </summary>
    [Table(nameof(MenuAction))]
    public class MenuAction : Entity
    {
        /// <summary>
        /// 动作名称
        /// </summary>
        [MaxLength(50), Required()]
        public string ActionName { get; set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        [Required]
        public ActionType ActionType { get; set; }
        /// <summary>
        /// 动作路径
        /// </summary>
        [MaxLength(50)]
        public string ActionUrl { get; set; }
        /// <summary>
        /// 动作状态
        /// </summary>
        [Required]
        public EnableEnum ActionStatus { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        [MaxLength(256), Required]
        public string Code { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(256)]
        public string Icon { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        [Required]
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }


    }
}
