using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


            /*try
            {*/

                DataTableCollection Tables = Variables.ServiceDB.Tables;

                if (Tables.Contains(DataGridName.Name))
                {
                    DataView TableView = new DataView();


                    for (int i = 0; i<Tables.Count; i++)
                    {
                        switch(DataGridName.Name)
                        {
                            case "employees":
                                TableView = Variables.GetTableDataByTableName("employees");
                                break;
                            case "fault_types":
                                TableView = Variables.GetTableDataByTableName("fault_types");
                                break;
                            case "orders":
                                TableView = Variables.GetTableDataByTableName("orders");
                                break;
                            case "parts":
                                TableView = Variables.GetTableDataByTableName("parts");
                                break;
                            case "parts_faults":
                                TableView = Variables.GetTableDataByTableName("parts_faults");
                                break;
                            case "positions":
                                TableView = Variables.GetTableDataByTableName("positions");
                                break;
                            case "repaired_models":
                                TableView = Variables.GetTableDataByTableName("repaired_models");
                                break;
                            case "served_shops":
                                TableView = Variables.GetTableDataByTableName("served_shops");
                                break;
                            default:
                                continue;
                        }
                        var MyDGName = Variables.FindMyDGByName(DataGridName.Name);
                        MyDGName.Name = DataGridName.Name;
                        MyDGName.TV = TableView;
                    }
                    DataGridName.ItemsSource = TableView;

                }
                else
                {
                    Console.WriteLine("Неопознанная таблица");
                }


            /*}
            catch (Exception err)
            {
                Notification.ShowError(err.Message);
            }*/


        }



    }
}
