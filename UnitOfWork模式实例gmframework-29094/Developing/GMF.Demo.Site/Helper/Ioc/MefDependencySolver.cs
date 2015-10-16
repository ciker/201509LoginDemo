// 源文件头信息：
// <copyright file="MefDependencySolver.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Site
// 最后修改：郭明锋
// 最后修改：2013/05/15 1:00
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace GMF.Demo.Site.Helper.Ioc
{
    public class MefDependencySolver : IDependencyResolver
    {
        private readonly ComposablePartCatalog _catalog;
        private const string MefContainerKey = "MefContainerKey";

        public MefDependencySolver(ComposablePartCatalog catalog)
        {
            _catalog = catalog;
        }

        public CompositionContainer Container
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(MefContainerKey))
                {
                    HttpContext.Current.Items.Add(MefContainerKey, new CompositionContainer(_catalog));
                }
                CompositionContainer container = (CompositionContainer)HttpContext.Current.Items[MefContainerKey];
                HttpContext.Current.Application["Container"] = container;
                return container;
            }
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            string contractName = AttributedModelServices.GetContractName(serviceType);
            return Container.GetExportedValueOrDefault<object>(contractName);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetExportedValues<object>(serviceType.FullName);
        }

        #endregion
    }
}