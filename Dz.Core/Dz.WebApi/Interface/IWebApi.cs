using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public interface IWebApi
    {
        RequestValues RequestParameters { get; set; }
    }
}
