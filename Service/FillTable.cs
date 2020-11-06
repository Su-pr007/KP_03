using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Service
{
    class FillTable
    {

        // sql - Текст MySQL запроса
        // DataGridName - Имя заполняемой таблицы
        public static void ByDG(string sql, DataGrid DataGridName)
        {

            DataGridName.ItemsSource = null;

            string login = Variables.DBlogin;
            string password = Variables.DBpassword;


            MySqlConnection conn = DBUtils.GetDBConnection(login, password);


            try
            {
                DataTable NewTable = new DataTable();
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(NewTable);
                DataGridName.IsReadOnly = false;
                DataGridName.ItemsSource = NewTable.DefaultView;
                DataGridName.IsReadOnly = true;
            }
            catch (Exception err)
            {
                Notification.ShowError(err.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }


        }


        /*public static void ByXElement(string sql, XElement DataTable)
        {

            DataTable.Attribute("ItemsSource").Value = null;

            string login = Variables.DBlogin;
            string password = Variables.DBpassword;


            // Подключение к бд
            MySqlConnection conn = DBUtils.GetDBConnection(login, password);


            try
            {
                DataTable NewTable = new DataTable();
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(NewTable);
                DataTable.Attribute("IsReadOnly").SetValue(false);
                DataTable.Attribute("ItemsSource").SetValue(NewTable.DefaultView);
                DataTable.Attribute("IsReadOnly").SetValue(true);
            }
            catch (Exception err)
            {
                Notification.ShowError(err.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }


        }*/
    }
}
