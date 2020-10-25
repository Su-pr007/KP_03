using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Service
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection(string login, string password)
        {
            string host = "127.0.0.1";
            int port = 3307;
            string database = "service";

            return DBMySQLUtils.GetDBConnection(host, port, database, login, password);
        }

    }
}
