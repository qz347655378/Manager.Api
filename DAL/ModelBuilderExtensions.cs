
using Common.Enum;
using Microsoft.EntityFrameworkCore;
using Models.System;


namespace DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder moduleBuilder)
        {
            //初始化一个超级管理员
            moduleBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Account = "admin",
                AccountStatus = EnableEnum.Enable,
                Id = 1,
                Mobile = "",
                Nickname = nameof(UserType.Administrator),
                RoleId = 1,
                UserType = UserType.Administrator
            });
            //初始化一个角色
            moduleBuilder.Entity<RoleInfo>().HasData(new RoleInfo
            {
                Id = 1,
                RoleName = nameof(UserType.Administrator),
            });
        }
    }
}
