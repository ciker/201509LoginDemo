// 源文件头信息：
// <copyright file="LoginInfo.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Core.Models
// 最后修改：郭明锋
// 最后修改：2013/05/14 23:47
// </copyright>

namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    ///     登录信息类
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        ///     获取或设置 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     获取或设置 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     获取或设置 IP地址
        /// </summary>
        public string IpAddress { get; set; }
    }
}