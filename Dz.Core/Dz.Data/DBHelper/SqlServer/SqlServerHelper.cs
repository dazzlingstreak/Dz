using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.Data
{
    public class SqlServerHelper : DBHelper
    {
        protected override DbProviderFactory factory => SqlClientFactory.Instance;
    }
}
