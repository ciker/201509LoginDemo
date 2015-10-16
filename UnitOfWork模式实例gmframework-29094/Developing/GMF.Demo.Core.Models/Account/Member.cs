// Դ�ļ�ͷ��Ϣ��
// <copyright file="Member.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core.Models
// ����޸ģ�������
// ����޸ģ�2013/05/14 23:15
// </copyright>

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using GMF.Component.Tools;
using GMF.Demo.Core.Models.Security;


namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    ///     ʵ���ࡪ���û���Ϣ
    /// </summary>
    [Description("�û���Ϣ")]
    public class Member : EntityBase<int>
    {
        public Member()
        {
            Roles = new List<Role>();
            LoginLogs = new List<LoginLog>();
        }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string NickName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// ��ȡ������ �û���չ��Ϣ
        /// </summary>
        public virtual MemberExtend Extend { get; set; }

        /// <summary>
        /// ��ȡ������ �û�ӵ�еĽ�ɫ��Ϣ����
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// ��ȡ������ �û���¼��¼����
        /// </summary>
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
    }
}