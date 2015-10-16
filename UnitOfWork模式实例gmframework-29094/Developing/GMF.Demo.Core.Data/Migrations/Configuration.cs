// Դ�ļ�ͷ��Ϣ��
// <copyright file="Configuration.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core.Data
// ����޸ģ�������
// ����޸ģ�2013/06/14 16:52
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
                new Role{ Name = "ϵͳ����", Description = "ϵͳ�����ɫ��ӵ������ϵͳ�Ĺ���Ȩ�ޡ�", RoleType = RoleType.Admin},
                new Role{ Name = "����", Description = "�����Ա��ɫ", RoleType = RoleType.User},
                new Role{ Name = "����", Description = "�����Ա��ɫ", RoleType = RoleType.User},
                new Role{ Name = "����", Description = "�����Ա��ɫ", RoleType = RoleType.User},
                new Role{ Name = "����", Description = "�����Ա��ɫ", RoleType = RoleType.User}
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();

            List<Member> members = new List<Member>
            {
                new Member { UserName = "admin", Password = "123456", Email = "admin@gmfcn.net", NickName = "����Ա" },
                new Member { UserName = "gmfcn", Password = "123456", Email = "mf.guo@qq.com", NickName = "������" }
            };

            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random((int)DateTime.Now.Ticks + i);
                Member member = new Member
                {
                    UserName = "userName" + i,
                    Password = "123456",
                    Email = "userName" + i + "@gmfcn.net",
                    NickName = "�û�" + i
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