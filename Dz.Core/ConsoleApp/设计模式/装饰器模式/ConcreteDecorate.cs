using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Decorate
{
    class SecureDecorate : Decorate
    {
        public SecureDecorate(IMessageWriter writer) : base(writer)
        {
        }

        public override void WriteMessage(string message)
        {
            Console.WriteLine($"经过SecureDecorate加持...");
            MsgWriter.WriteMessage(message);
        }
    }

    class EncryptedDecorate : Decorate
    {

        public EncryptedDecorate(IMessageWriter writer) : base(writer)
        {
        }

        public override void WriteMessage(string message)
        {
            Console.WriteLine($"经过EncryptedDecorate加持...");
            MsgWriter.WriteMessage(message);
        }
    }

    class CompressedDecorate : Decorate
    {

        public CompressedDecorate(IMessageWriter writer) : base(writer)
        {
        }

        public override void WriteMessage(string message)
        {
            Console.WriteLine($"经过CompressedDecorate加持...");
            MsgWriter.WriteMessage(message);
        }
    }

    class DigitallySignedDecorate : Decorate
    {
        public DigitallySignedDecorate(IMessageWriter writer) : base(writer)
        {
        }

        public override void WriteMessage(string message)
        {
            Console.WriteLine($"经过DigitallySignedDecorate加持...");
            MsgWriter.WriteMessage(message);
        }
    }

}
