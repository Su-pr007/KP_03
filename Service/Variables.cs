using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Variables
    {
        public static string login;
        public static string password;

        public static int ProfileId;

        public static MySqlConnection conn = DBUtils.GetDBConnection(login, password);


        public static void Clear()
        {
            login = "";
            password = "";
            ProfileId = 0;
        }
    }
}
