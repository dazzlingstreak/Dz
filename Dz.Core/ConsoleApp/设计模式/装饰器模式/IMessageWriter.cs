using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Decorate
{
    public interface IMessageWriter
    {
        void WriteMessage(string message);
    }
}
