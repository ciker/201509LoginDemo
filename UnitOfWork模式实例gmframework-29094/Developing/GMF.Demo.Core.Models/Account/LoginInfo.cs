// Դ�ļ�ͷ��Ϣ��
// <copyright file="LoginInfo.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core.Models
// ����޸ģ�������
// ����޸ģ�2013/05/14 23:47
// </copyright>

namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    ///     ��¼��Ϣ��
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        ///     ��ȡ������ ��¼�˺�
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     ��ȡ������ ��¼����
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     ��ȡ������ IP��ַ
        /// </summary>
        public string IpAddress { get; set; }
    }
}