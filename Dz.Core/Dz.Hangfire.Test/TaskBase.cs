using Dz.Core;
using Dz.Hangfire.Scheduler;
using Hangfire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Hangfire.Test
{
    public abstract class TaskBase : ITask
    {
        public virtual string Queue => "default";

        public virtual string Cron => "*/10 * * * *";

        /// <summary>
        /// 业务逻辑实现，在Perform中被调用
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        protected abstract Task PerformAsync(Guid guid);

        /// <summary>
        /// 任务失败，发送邮件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        protected void SendEmail(string title, string content)
        {
            title = title + "from " + Environment.MachineName + " at " + DateTime.Now;
            var recipients = ConfigurationManager.AppSettings["EmailRecipients"];
            foreach (var item in recipients.Split(','))
            {
                new EmailClient()
                {
                    LoginName = ConfigurationManager.AppSettings["EmailFromAddress"],
                    Password = ConfigurationManager.AppSettings["EmailFromPassword"],
                    SmtpHost = ConfigurationManager.AppSettings["SmtpHost"]
                }.SendMessage(item, title, content);
            }
        }

        private bool _isDoing;
        private readonly static Guid _guid = Guid.NewGuid();

        /// <summary>
        /// 任务调度实际方法
        /// </summary>
        public void Perform()
        {
            if (_isDoing)
            {
                return;
            }

            _isDoing = true;

            try
            {
                PerformAsync(_guid).Wait();
            }
            catch (Exception ex)
            {
                var message = $"{GetType().Name}，{ex.Message}";
                SendEmail(message, ex.ToString());

                throw;
            }
            finally
            {
                _isDoing = false;
            }
        }
    }
}
