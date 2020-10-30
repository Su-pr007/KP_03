using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Service
{
    class Variables
    {
        public static string DBlogin;
        public static string DBpassword;

        public static int ProfileId;

        public static MySqlConnection conn;
        public static TablesWindow TablesWindow_Window;
        public static ReportWindow ReportWindow_Window;
        public static LoginWindow LoginWindow_Window;
        public static MenuWindow MenuWindow_Window;

        public static List<DataGrid> DataGrids;


        public static void InitVariables()
        {
            conn = DBUtils.GetDBConnection(DBlogin, DBpassword);

            TablesWindow_Window = new TablesWindow();
            ReportWindow_Window = new ReportWindow();
            LoginWindow_Window = new LoginWindow();
            MenuWindow_Window = new MenuWindow();

            DataGrids = new List<DataGrid>();
        }


        public static void ClearVariables()
        {
            DBlogin = "";
            DBpassword = "";
            ProfileId = 0;

            TablesWindow_Window = null;
            ReportWindow_Window = null;
            LoginWindow_Window = null;
            MenuWindow_Window = null;

            DataGrids = null;
        }





        public static void ApplicationStop()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
