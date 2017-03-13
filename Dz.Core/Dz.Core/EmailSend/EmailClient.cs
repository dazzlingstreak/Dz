using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Core
{
    public class EmailClient
    {
        /// <summary>
        /// 电子邮箱登陆账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 电子邮箱登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SMTP主机地址
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendMessage(string recipients, string subject, string body)
        {
            var smtpClient = new SmtpClient()
            {
                Credentials = new NetworkCredential(LoginName, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = SmtpHost,
                Port = 25
            };

            smtpClient.Send(LoginName, recipients, subject, body);
        }
    }
}
