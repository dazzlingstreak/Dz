using Dz.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisProvider.StringSet("Dz.Redis", "ah yo", TimeSpan.FromMinutes(1));
            Console.WriteLine(RedisProvider.StringGet("Dz.Redis"));
            Console.ReadLine();
        }
    }
}
