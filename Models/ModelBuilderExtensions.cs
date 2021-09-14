
using System;
using System.Collections.Generic;
using Common.Enum;
using Microsoft.EntityFrameworkCore;
using Models.System;


namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="moduleBuilder"></param>
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
                RoleStatus = EnableEnum.Enable
            });

            //初始化菜单

            moduleBuilder.Entity<MenuAction>().HasData(new List<MenuAction>
            {
                new MenuAction
                {
                    ActionName = "系统管理",
                    ActionStatus = EnableEnum.Enable,
                    ActionType = ActionType.Menu,
                    ActionUrl = "/",
                    Code = "System",
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    Id = 1,
                    Icon = "layui-icon-set",
                    IsDelete = DeleteStatus.NoDelete,
                    ParentId = 0,
                    Sort = 1
                },
                new MenuAction
                {
                    ActionName = "用户管理",
                    ActionStatus = EnableEnum.Enable,
                    ActionType = ActionType.Menu,
                    ActionUrl = "/System/User",
                    Code = "System.User",
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    Id = 2,
                    Icon = "",
                    IsDelete = DeleteStatus.NoDelete,
                    ParentId = 1,
                    Sort = 1
                },
                new MenuAction
                {
                    ActionName = "角色管理",
                    ActionStatus = EnableEnum.Enable,
                    ActionType = ActionType.Menu,
                    ActionUrl = "/System/Role",
                    Code = "System.Role",
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    Id = 3,
                    Icon = "",
                    IsDelete = DeleteStatus.NoDelete,
                    ParentId = 1,
                    Sort = 2
                },
                new MenuAction
                {
                    ActionName = "菜单管理",
                    ActionStatus = EnableEnum.Enable,
                    ActionType = ActionType.Menu,
                    ActionUrl = "/System/Menu",
                    Code = "System.Menu",
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    Id = 4,
                    Icon = "",
                    IsDelete = DeleteStatus.NoDelete,
                    ParentId = 1,
                    Sort = 3
                },
            });

        }
    }
}
