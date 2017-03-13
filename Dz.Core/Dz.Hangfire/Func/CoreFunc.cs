using Dz.Hangfire.Scheduler;
using Hangfire;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dz.Hangfire.Func
{
    public class CoreFunc
    {
        //public static IKernel Kernerl => new StandardKernel();

        /// <summary>
        /// 任务加载
        /// </summary>
        public static void LoanTasks()
        {
            var configurations = AssemblyLoader.Load(); //加载【bin/系统目录/】下的json文件+dll文件
            if (configurations != null)
            {
                foreach (var configuration in configurations)
                {
                    var tasks = new List<ITask>();  //需引用Dz.Hangfire.Scheduler，这个是接口约定dll，供实际任务业务实现

                    //读取任务配置json信息，获取任务实例
                    foreach (var item in configuration.Items)
                    {
                        Type taskInstanceType = null;
                        if (string.IsNullOrEmpty(item.From))
                        {
                            continue;
                        }

                        if (item.From == item.To || item.To.ToLower() == "self" || string.IsNullOrEmpty(item.To))
                        {
                            taskInstanceType = Type.GetType(item.From);
                        }
                        else
                        {
                            taskInstanceType = Type.GetType(item.To);
                        }

                        if (typeof(ITask).IsAssignableFrom(taskInstanceType) && taskInstanceType.IsPublic && !taskInstanceType.IsAbstract)
                        {
                            var taskInstance = Activator.CreateInstance(taskInstanceType) as ITask;
                            if (taskInstance != null)
                            {
                                tasks.Add(taskInstance);
                            }
                        }
                    }

                    foreach (var task in tasks)
                    {
                        RecurringJob.AddOrUpdate(() => task.Perform(), task.Cron, TimeZoneInfo.Local, task.Queue.ToLower());
                    }
                }
            }
        }
    }
}