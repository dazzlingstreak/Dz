using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Dz.Hangfire
{
    public static class AssemblyLoader
    {
        private static string _defaultJsonFile = "injection.json";

        /// <summary>
        /// 加载JSON文件，配置了各个系统任务的dll命名
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <returns></returns>
        internal static List<TaskConfiguration> Load(string jsonFileName = null)
        {
            jsonFileName = jsonFileName ?? _defaultJsonFile;

            var binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");  //bin目录
            var subPaths = Directory.GetDirectories(binPath, "*", SearchOption.TopDirectoryOnly);//子文件夹目录
            if (subPaths.Length > 0)
            {
                //解析dll失败事件
                AppDomain.CurrentDomain.AssemblyResolve += ResolveEventHandler;
            }

            var configurations = new List<TaskConfiguration>();
            foreach (var subPath in subPaths)
            {
                var path = Path.Combine(subPath, jsonFileName);
                if (File.Exists(path))
                {
                    var configuration = JsonConvert.DeserializeObject<TaskConfiguration>(File.ReadAllText(path));
                    configurations.Add(configuration);
                }
            }

            return configurations;
        }

        /// <summary>
        /// 分部解析子文件夹下的DLL，不用把各个其他系统的任务相关DLL都放在主Bin下，分开放置，管理清晰，但是在加载bin的dll后，启动任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static Assembly ResolveEventHandler(object sender, ResolveEventArgs args)
        {
            var binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");  //bin目录
            var name = args.Name;
            if (name.Contains(","))
            {
                name = name.Substring(0, name.IndexOf(","));  //获得解析失败的程序集dll名称
            }
            var files = Directory.GetFiles(binPath, name + ".dll", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                return AssemblyCache.Load(files[0]);
            }
            return null;
        }
    }
}