using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Test.Model.Common
{
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public SexType Sex { get; set; }
    }

    public enum SexType
    {
        Male = 1,
        Female = 2
    }
}
