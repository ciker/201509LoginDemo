using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using GMF.Component.Tools;


namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    /// ʵ���ࡪ����¼��¼��Ϣ
    /// </summary>
    [Description("��¼��¼��Ϣ")]
    public class LoginLog : EntityBase<Guid>
    {
        /// <summary>
        /// ��ʼ��һ�� ��¼��¼ʵ���� ����ʵ��
        /// </summary>
        public LoginLog()
        {
            Id = CombHelper.NewComb();
        }

        [Required]
        [StringLength(15)]
        public string IpAddress { get; set; }

        /// <summary>
        /// ��ȡ������ �����û���Ϣ
        /// </summary>
        public virtual Member Member { get; set; }
    }
}
