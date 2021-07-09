using Microsoft.EntityFrameworkCore;
using Models.System;

namespace Models
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class ManagerDbContext : DbContext
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        /// <param name="options"></param>
        public ManagerDbContext(DbContextOptions options) : base(options)
        {
               
        }





        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserInfo> UserInfos { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<RoleInfo> RoleInfos { get; set; }

        /// <summary>
        /// 角色菜单表
        /// </summary>
        public DbSet<RoleAction> RoleActions { get; set; }

        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<MenuAction> MenuActions { get; set; }


        /// <summary>
        /// 当表被创建时
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }


}
