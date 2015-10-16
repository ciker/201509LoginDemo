// Դ�ļ�ͷ��Ϣ��
// <copyright file="Role.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core.Models
// ����޸ģ�������
// ����޸ģ�2013/05/21 15:26
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using GMF.Component.Tools;
using GMF.Demo.Core.Models.Account;


namespace GMF.Demo.Core.Models.Security
{
    /// <summary>
    ///     ʵ���ࡪ����ɫ��Ϣ
    /// </summary>
    [Description("��ɫ��Ϣ")]
    public class Role : EntityBase<Guid>
    {
        public Role()
        {
            Id = CombHelper.NewComb();
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// ��ȡ������ ��ɫ����
        /// </summary>
        public RoleType RoleType
        {
            get { return (RoleType)RoleTypeNum; }
            set { RoleTypeNum = (int)value; }
        }

        /// <summary>
        /// ��ȡ������ ��ɫ���͵���ֵ��ʾ���������ݿ�洢
        /// </summary>
        public int RoleTypeNum { get; set; }

        /// <summary>
        ///     ��ȡ������ ӵ�д˽�ɫ���û���Ϣ����
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }
    }
}
