// 源文件头信息：
// <copyright file="Program.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Consoles
// 最后修改：郭明锋
// 最后修改：2013/05/14 15:28
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using GMF.Component.Data;
using GMF.Component.Tools;
using GMF.Demo.Core.Data.Initialize;
using GMF.Demo.Core.Data.Repositories.Account;
using GMF.Component.Tools.T4;
using GMF.Demo.Core.Models.Account;
using GMF.Demo.Site;
using GMF.Demo.Site.Helper.Logging;


namespace GMF.Demo.Consoles
{
    [Export]
    internal class Program
    {
        private static CompositionContainer _container;

        [Import]
        public IAccountSiteContract AccountContract { get; set; }

        #region 主程序

        private static void Main(string[] args)
        {
            //初始化MEF组合容器
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Directory.GetCurrentDirectory()));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            _container = new CompositionContainer(catalog);
            DatabaseInitializer.Initialize();

            bool exit = false;
            while (true)
            {
                try
                {
                    Console.WriteLine("请输入命令：0; 退出程序，功能命令：1 - n");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }
                    switch (input.ToLower())
                    {
                        case "0":
                            exit = true;
                            break;
                        case "1":
                            Method01();
                            break;
                        case "2":
                            Method02();
                            break;
                        case "3":
                            Method03();
                            break;
                        case "4":
                            Method04();
                            break;
                        case "5":
                            Method05();
                            break;
                        case "6":
                            Method06();
                            break;
                        case "7":
                            Method07();
                            break;
                        case "8":
                            Method08();
                            break;
                        case "9":
                            Method09();
                            break;
                        case "10":
                            Method10();
                            break;
                        case "11":
                            Method11();
                            break;
                        case "12":
                            Method12();
                            break;
                        case "13":
                            Method13();
                            break;
                        case "14":
                            Method14();
                            break;
                    }
                    if (exit)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    ExceptionHandler(e);
                }
            }
        }

        private static void ExceptionHandler(Exception e)
        {
            ExceptionMessage emsg = new ExceptionMessage(e);
            Console.WriteLine(emsg.ErrorDetails);
        }

        #endregion

        #region 功能方法

        private static void Method01()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            List<Member> members = memberRepository.Entities.ToList();
            foreach (var member in members)
            {
                Console.WriteLine(member.Roles.Count);
            }
        }

        private static void Method02()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            List<Member> members = memberRepository.Entities.Include(m => m.Roles).ToList();
            foreach (var member in members)
            {
                Console.WriteLine(member.Roles.Count);
            }
        }

        private static void Method03()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            var members = memberRepository.Entities.Select(m => new
            {
                Roles = new { Count = m.Roles.Count }
            }).ToList();
            foreach (var member in members)
            {
                Console.WriteLine(member.Roles.Count);
            }
        }

        private static void Method04()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("AddDate", ListSortDirection.Descending), new PropertySortCondition("UserName") };
            int total;
            var members = memberRepository.Entities.Where<Member, int>(m => true, 1, 15, out total, sortConditions).ToList();
            foreach (var member in members)
            {
                Console.WriteLine(member.NickName);
            }
        }

        private static void Method05()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            Member member = memberRepository.Entities.First();
            member.AddDate = DateTime.Now;
            memberRepository.Update(member);
            Console.WriteLine(memberRepository.Entities.First().AddDate);
        }

        private static void Method06()
        {
            IMemberRepository memberRepository = _container.GetExportedValue<IMemberRepository>();
            Member member = new Member {Id = 1, AddDate = DateTime.Now};
            memberRepository.Update(m => new {m.AddDate}, member);
            Console.WriteLine(memberRepository.Entities.First().AddDate);
        }

        private static void Method07()
        {
            throw new NotImplementedException();
        }

        private static void Method08()
        {
            throw new NotImplementedException();
        }

        private static void Method09()
        {
            throw new NotImplementedException();
        }

        private static void Method10()
        {
            throw new NotImplementedException();
        }

        private static void Method11()
        {
            throw new NotImplementedException();
        }

        private static void Method12()
        {
            throw new NotImplementedException();
        }

        private static void Method13()
        {
            throw new NotImplementedException();
        }

        private static void Method14()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}