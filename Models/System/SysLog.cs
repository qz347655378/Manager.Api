using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.System
{
    [Table(nameof(SysLog))]
    public class SysLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        [Required]
        public string Message { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        [Required]
        public string Level { get; set; }
        /// <summary>
        /// 传入时间
        /// </summary>
        [Required]
        public DateTime Timestampp { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// 请求的API
        /// </summary>
        public string Api { get; set; }
        /// <summary>
        /// API回复状态
        /// </summary>
        public string ResponseStatus { get; set; }
        /// <summary>
        /// 客户端请求方式
        /// </summary>
        public string RequestMethod { get; set; }
    }
}
