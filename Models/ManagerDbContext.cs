using System;
using System.Threading;
using System.Threading.Tasks;
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


        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //保存失败，用于处理并发
                return -1;
            }


        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                //出现并发异常,乐观锁
                return Task.FromResult(-1);
            }

        }


    }



}
