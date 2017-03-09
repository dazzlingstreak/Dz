using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.IOC.Model
{
    /// <summary>
    /// 俱乐部接口
    /// </summary>
    public interface IClub
    {
        void Register();

        void PlayerIntroduction();

        void Train();

        void Competition();
    }
}
