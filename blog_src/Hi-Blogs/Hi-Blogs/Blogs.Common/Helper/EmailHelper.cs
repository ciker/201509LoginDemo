using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Blogs.Helper
{
    public class EmailHelper
    {

        #region Eail 属性

        private string _mailFrom = "system@haojima.net";
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get { return _mailFrom; } set { _mailFrom = value; } }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] mailToArray { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string[] mailCcArray { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailPwd { get; set; }

        private string _host = "smtp.haojima.net";
        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string host { get { return _host; } set { _host = value; } }

        private bool _isbodyHtml = true;
        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get { return _isbodyHtml; } set { _isbodyHtml = value; } }

        private string _nickname = "嗨-博客 系统通知";
        /// <summary>
        /// 发送者昵称
        /// </summary>
        public string nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public string[] attachmentsPath { get; set; }

        //优先级别
        private MailPriority _Priority = MailPriority.Normal;
        /// <summary>
        /// 优先级别  默认正常优先级
        /// </summary>
        public MailPriority Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
            }
        }
        #endregion

        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(mailFrom, nickname);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();

            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            //邮件优先级
            myMail.Priority = Priority;

            myMail.IsBodyHtml = isbodyHtml;

            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);//115                 //设置SMTP邮件服务器
            smtp.Host = host;
            // smtp.EnableSsl = true;
            //smtp.Port = 587;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
            }

        }
    }
}
