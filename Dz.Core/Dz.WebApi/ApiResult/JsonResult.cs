using blqw.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.WebApi
{
    public class JsonResult : StringResult
    {
        public int Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public DateTime ServerTime { get; set; }

        public JsonResult(object data) : base()
        {
            Data = data;
            ServerTime = DateTime.Now;
            Message = "";
            Code = (int)ExceptionCode.None;
        }

        public override string ToString()
        {
            return ComponentServices.ToJsonString(this);
        }
    }
}
