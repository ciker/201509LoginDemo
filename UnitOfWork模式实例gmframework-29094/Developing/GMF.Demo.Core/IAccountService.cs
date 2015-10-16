// Դ�ļ�ͷ��Ϣ��
// <copyright file="IAccountService.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core
// ����޸ģ�������
// ����޸ģ�2013/05/27 23:06
// </copyright>

using System.Linq;

using GMF.Component.Tools;
using GMF.Demo.Core.Data.Repositories;
using GMF.Demo.Core.Models;
using GMF.Demo.Core.Models.Account;
using GMF.Demo.Core.Models.Security;


namespace GMF.Demo.Core
{
    /// <summary>
    ///     �˻�ģ�����ҵ����Լ
    /// </summary>
    public interface IAccountContract
    {
        #region ����

        /// <summary>
        /// ��ȡ �û���Ϣ��ѯ���ݼ�
        /// </summary>
        IQueryable<Member> Members { get; }

        /// <summary>
        /// ��ȡ �û���չ��Ϣ��ѯ���ݼ�
        /// </summary>
        IQueryable<MemberExtend> MemberExtends { get; }

        /// <summary>
        /// ��ȡ ��¼��¼��Ϣ��ѯ���ݼ�
        /// </summary>
        IQueryable<LoginLog> LoginLogs { get; }

        /// <summary>
        /// ��ȡ ��ɫ��Ϣ��ѯ���ݼ�
        /// </summary>
        IQueryable<Role> Roles { get; }

        #endregion

        #region ��������

        /// <summary>
        ///     �û���¼
        /// </summary>
        /// <param name="loginInfo">��¼��Ϣ</param>
        /// <returns>ҵ��������</returns>
        OperationResult Login(LoginInfo loginInfo);

        #endregion
    }
}