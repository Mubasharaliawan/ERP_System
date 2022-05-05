using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ERP.Connection
{
     public class DbConnection
    {
        public  string path;

       public  DbConnection()
        {
            path = "Data Source=.;Initial Catalog=ERP;Integrated Security=True";
        }

    }

}
