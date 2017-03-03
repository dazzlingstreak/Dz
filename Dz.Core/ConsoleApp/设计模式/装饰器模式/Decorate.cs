using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Decorate
{
    public abstract class Decorate : IMessageWriter
    {
        public IMessageWriter MsgWriter { get; set; }

        public Decorate(IMessageWriter writer)
        {
            MsgWriter = writer;
        }

        public abstract void WriteMessage(string message);
        //public void WriteMessage(string message)
        //{
        //    MsgWriter.WriteMessage(message);
        //}
    }
}
