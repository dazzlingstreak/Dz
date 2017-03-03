using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Proxy
{
    /// <summary>
    /// 代理
    /// </summary>
    public class ProxyImage : IImage
    {
        public string FileName { get; set; }
        private IImage image;

        public ProxyImage(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// 实现接口方法
        /// </summary>
        public void Display()
        {
            if (image == null)
            {
                //代理模式，在编译时就确定使用哪个真实类，client端在调用时，只知道代理，并不需要知道真实
                image = new RealImage(FileName);
            }
            image.Display();
        }

    }
}
