// 源文件头信息：
// <copyright file="Configuration.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Core.Data
// 最后修改：郭明锋
// 最后修改：2013/06/14 16:52
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using GMF.Component.Data;
using GMF.Demo.Core.Models.Account;
using GMF.Demo.Core.Models.Security;


namespace GMF.Demo.Core.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            List<Role> roles = new List<Role>
            {
                new Role{ Name = "系统管理", Description = "系统管理角色，拥有整个系统的管理权限。", RoleType = RoleType.Admin},
                new Role{ Name = "蓝钻", Description = "蓝钻会员角色", RoleType = RoleType.User},
                new Role{ Name = "红钻", Description = "红钻会员角色", RoleType = RoleType.User},
                new Role{ Name = "黄钻", Description = "黄钻会员角色", RoleType = RoleType.User},
                new Role{ Name = "绿钻", Description = "绿钻会员角色", RoleType = RoleType.User}
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();

            List<Member> members = new List<Member>
            {
                new Member { UserName = "admin", Password = "123456", Email = "admin@gmfcn.net", NickName = "管理员" },
                new Member { UserName = "gmfcn", Password = "123456", Email = "mf.guo@qq.com", NickName = "郭明锋" }
            };

            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random((int)DateTime.Now.Ticks + i);
                Member member = new Member
                {
                    UserName = "userName" + i,
                    Password = "123456",
                    Email = "userName" + i + "@gmfcn.net",
                    NickName = "用户" + i
                };
                var roleArray = roleSet.ToArray();
                member.Roles.Add(roleArray[rnd.Next(0, roleArray.Length)]);
                if (rnd.NextDouble() > 0.5)
                {
                    member.Roles.Add(roleArray[rnd.Next(1, roleArray.Length)]);                    
                }
                members.Add(member);
            }
            DbSet<Member> memberSet = context.Set<Member>();
            memberSet.AddOrUpdate(m => new { m.UserName }, members.ToArray());
        }
    }
}