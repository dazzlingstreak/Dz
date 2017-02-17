using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Data
{
    public class MySqlHelper : DBHelper
    {
        protected override DbProviderFactory factory => MySqlClientFactory.Instance;
    }
}
