// Դ�ļ�ͷ��Ϣ��
// <copyright file="MemberExtend.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR�汾��4.0.30319.239
// ������֯��������@�й�
// ��˾��վ��http://www.gmfcn.net
// �������̣�GMF.Demo.Core.Models
// ����޸ģ�������
// ����޸ģ�2013/05/20 13:43
// </copyright>

using System;
using System.ComponentModel;

using GMF.Component.Tools;


namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    ///     ʵ���ࡪ���û���չ��Ϣ
    /// </summary>
    [Description("�û���չ��Ϣ")]
    public class MemberExtend : EntityBase<Guid>
    {
        /// <summary>
        /// ��ʼ��һ�� �û���չʵ���� ����ʵ��
        /// </summary>
        public MemberExtend()
        {
            Id = CombHelper.NewComb();
        }

        public string Tel { get; set; }

        public MemberAddress Address { get; set; }

        public virtual Member Member { get; set; }
    }
}