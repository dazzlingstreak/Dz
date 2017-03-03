using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Proxy
{
    class RealImage : IImage
    {
        public string FileName { get; set; }

        public RealImage(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// 实现接口方法
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"Displaying  {FileName}");
        }

        /// <summary>
        /// 自有方法
        /// </summary>
        /// <param name="fileName"></param>
        public void LoanFromDisk(string fileName)
        {
            Console.WriteLine($"LoanFromDisk  {fileName}");
        }
    }
}
