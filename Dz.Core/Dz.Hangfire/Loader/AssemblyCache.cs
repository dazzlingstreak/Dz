using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Dz.Hangfire
{
    public static class AssemblyCache
    {
        private static Dictionary<string, Assembly> _cache = new Dictionary<string, Assembly>();

        internal static Assembly Load(string assemblyFile)
        {
            lock (_cache)
            {
                try
                {
                    Assembly assembly = null;
                    if (_cache.ContainsKey(assemblyFile))
                    {
                        return _cache[assemblyFile];
                    }
                    else
                    {
                        assembly = Assembly.LoadFrom(assemblyFile);
                        _cache.Add(assemblyFile, assembly);
                    }
                    return assembly;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}