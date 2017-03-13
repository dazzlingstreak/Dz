using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.Dashboard;
using System.Configuration;
using Dz.Hangfire.Configuration;
using System.Web;
using System.Linq;
using Dz.Hangfire.Func;

[assembly: OwinStartup(typeof(Dz.Hangfire.Startup))]

namespace Dz.Hangfire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //读取任务调度的配置信息Section，配置了redis的存储信息和执行队列的信息，各个任务在加入hangfire时，会分配在相应设置的queue队列，若执行队列未初始化，那么任务不能执行
            var taskScheduler = ConfigurationManager.GetSection("taskscheduler") as TaskSchedulerSection;

            GlobalConfiguration.Configuration.UseRedisStorage(taskScheduler.Redis.ConnectionString)
                .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                .UseDashboardMetric(DashboardMetrics.SucceededCount)
                .UseDashboardMetric(DashboardMetrics.FailedCount)
                .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                .UseDashboardMetric(DashboardMetrics.RetriesCount)
                .UseDashboardMetric(DashboardMetrics.RecurringJobCount);

            //Hangfire服务器配置，后台任务选项：队列初始化
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                Queues = taskScheduler.Queues.Value.Split(',').Select(p => p.ToLower()).ToArray(),
                WorkerCount = Environment.ProcessorCount * 5
            });

            //5次重试后不成功，进入fail
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

            //仪表板显示配置
            app.UseHangfireDashboard("/hangfire", new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~") + "hangfire" });
            //加载任务
            CoreFunc.LoanTasks();

        }
    }
}
