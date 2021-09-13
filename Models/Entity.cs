using Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{

    /// <summary>
    /// 实体基类
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 编辑时间
        /// </summary>
        [Required]
        public DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public DeleteStatus IsDelete { get; set; } = DeleteStatus.NoDelete;


    }
}
