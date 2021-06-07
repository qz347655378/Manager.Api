using Microsoft.EntityFrameworkCore;
using Models.System;

namespace Models
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<RoleInfo> RoleInfos { get; set; }

        public DbSet<RoleAction> RoleActions { get; set; }

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
