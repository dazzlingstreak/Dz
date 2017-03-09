using Novacode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dz.Test
{
    public class DocX_Test
    {
        [Fact]
        public void TestMethod()
        {
            using (var doc = DocX.Create(@"HelloWorld.docx"))
            {
                doc.Save();
            }

            DocX document = DocX.Load(@"HelloWorld.docx");
        }
    }
}
