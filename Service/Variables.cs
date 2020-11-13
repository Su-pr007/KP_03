using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Service
{

    class MyDataGrid
    {
        public string Name;

        // TableView
        public DataView TV;

        // DataGrid
        public DataGrid DG;

        // Названия колонок на английском
        public List<string> ColumnsEng;

        // Primary Key
        public string PK;

    }

    class Variables
    {
        public static string DBlogin;
        public static string DBpassword;

        public static serviceDataSet ServiceDB = new serviceDataSet();

        public static int ProfileId;

        public static MySqlConnection conn;

        public static TablesWindow TablesWindow_Window;
        public static ReportWindow ReportWindow_Window;
        public static LoginWindow LoginWindow_Window;
        public static MenuWindow MenuWindow_Window;

        public static List<DataGrid> DataGrids;
        public static List<MyDataGrid> MyDGs;
        public static DateTime DateTime1 = new DateTime();

        public static Dictionary<string, string> ColumnsDictionary = new Dictionary<string, string>()
        {};



        


        public static void InitVariables()
        {
            conn = DBUtils.GetDBConnection(DBlogin, DBpassword);


            TablesWindow_Window = new TablesWindow();
            ReportWindow_Window = new ReportWindow();
            LoginWindow_Window = new LoginWindow();
            MenuWindow_Window = new MenuWindow();

            DataGrids = new List<DataGrid>();
            MyDGs = new List<MyDataGrid>();

            ColumnsDictionary = new Dictionary<string, string> { };

            // employees
            ColumnsDictionary.Add("e_id", "Код сотрудника");
            ColumnsDictionary.Add("e_surname", "Фамилия");
            ColumnsDictionary.Add("e_name_patronymic", "Имя, Отчество");
            ColumnsDictionary.Add("e_age", "Возраст");
            ColumnsDictionary.Add("e_sex", "Пол");
            ColumnsDictionary.Add("e_address", "Адрес");
            ColumnsDictionary.Add("e_phone", "Номер телефона");
            ColumnsDictionary.Add("e_passport_series_and_number", "Серия и номер паспорта");
            ColumnsDictionary.Add("e_passport_issued_by", "Кем выдан паспорт");
            ColumnsDictionary.Add("e_passport_date_of_issue", "Дата выдачи паспорта");
            ColumnsDictionary.Add("e_p_id", "Код должности");

            // fault_types
            ColumnsDictionary.Add("ft_id", "ID");
            ColumnsDictionary.Add("ft_model_id", "Id модели");
            ColumnsDictionary.Add("ft_description", "Описание");
            ColumnsDictionary.Add("ft_symptomes", "Симптомы");
            ColumnsDictionary.Add("ft_repair_methods", "Методы ремонта");
            ColumnsDictionary.Add("ft_price", "Стоимость работы");

            // orders
            ColumnsDictionary.Add("o_id", "ID заказа");
            ColumnsDictionary.Add("o_order_date", "Дата заказа");
            ColumnsDictionary.Add("o_pick_date", "Дата возврата");
            ColumnsDictionary.Add("o_name", "ФИО заказчика");
            ColumnsDictionary.Add("o_serial_number", "Серийный номер");
            ColumnsDictionary.Add("o_ft_id", "Код вида неисправности");
            ColumnsDictionary.Add("o_ss_id", "Код магазина");
            ColumnsDictionary.Add("o_guarantee", "Отметка о гарантии");
            ColumnsDictionary.Add("o_guarantee_period", "Срок гарантии (месяцев)");
            ColumnsDictionary.Add("o_price", "Итоговая цена заказа");
            ColumnsDictionary.Add("o_e_id", "Код сотрудника");

            // parts
            ColumnsDictionary.Add("part_id", "Код запчасти");
            ColumnsDictionary.Add("part_name", "Наименование запчасти");
            ColumnsDictionary.Add("part_functions", "Функции");
            ColumnsDictionary.Add("part_price", "Цена запчасти");

            // parts_faults
            ColumnsDictionary.Add("pf_id", "ID");
            ColumnsDictionary.Add("pf_fault_id", "Код вида неисправности");
            ColumnsDictionary.Add("pf_part_id", "Код запчасти");

            // positions
            ColumnsDictionary.Add("p_id", "Код должности");
            ColumnsDictionary.Add("p_name", "Наименование должности");
            ColumnsDictionary.Add("p_salary", "Оклад");
            ColumnsDictionary.Add("p_duties", "Обязанности");
            ColumnsDictionary.Add("p_requirements", "Требования");

            // repaired_models
            ColumnsDictionary.Add("rm_id", "Код модели");
            ColumnsDictionary.Add("rm_name", "Наименование");
            ColumnsDictionary.Add("rm_type", "Тип");
            ColumnsDictionary.Add("rm_performance", "Производительность");
            ColumnsDictionary.Add("rm_tech_param", "Технические характеристики");
            ColumnsDictionary.Add("rm_specials", "Особенности");

            // served_shops
            ColumnsDictionary.Add("ss_id", "Код магазина");
            ColumnsDictionary.Add("ss_name", "Наименование магазина");
            ColumnsDictionary.Add("ss_address", "Адрес магазина");
            ColumnsDictionary.Add("ss_phone_number", "Номер телефона магазина");
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
            MyDGs = null;

            ColumnsDictionary = null;
        }


        public static MyDataGrid FindMyDGByName(string TableName)
        {
            foreach(MyDataGrid curTable in MyDGs)
            {
                if (curTable.DG.Name == TableName) return curTable;
            }
            return null;
        }



        public static DataView GetTableDataByTableName(string TableName)
        {


            DataView TableData;
            
            switch (TableName)
            {
                case "employees":
                    TableData = new serviceDataSetTableAdapters.employeesTableAdapter().GetData().DefaultView;
                    break;
                case "fault_types":
                    TableData = new serviceDataSetTableAdapters.fault_typesTableAdapter().GetData().DefaultView;
                    break;
                case "orders":
                    TableData = new serviceDataSetTableAdapters.ordersTableAdapter().GetData().DefaultView;
                    break;
                case "parts":
                    TableData = new serviceDataSetTableAdapters.partsTableAdapter().GetData().DefaultView;
                    break;
                case "parts_faults":
                    TableData = new serviceDataSetTableAdapters.parts_faultsTableAdapter().GetData().DefaultView;
                    break;
                case "positions":
                    TableData = new serviceDataSetTableAdapters.positionsTableAdapter().GetData().DefaultView;
                    break;
                case "repaired_models":
                    TableData = new serviceDataSetTableAdapters.repaired_modelsTableAdapter().GetData().DefaultView;
                    break;
                case "served_shops":
                    TableData = new serviceDataSetTableAdapters.served_shopsTableAdapter().GetData().DefaultView;
                    break;
                default:
                    TableData = new serviceDataSetTableAdapters.employeesTableAdapter().GetData().DefaultView;
                    break;
            }

            DataTable RusColumnsTable = new DataTable();
            RusColumnsTable = TranslateColumns(TableData.Table);



            return RusColumnsTable.DefaultView;
        }

        public static DataTable TranslateColumns(DataTable TableFrom)
        {
            DataTable TableTo = new DataTable();
            MyDataGrid thisDG = FindMyDGByName(TableFrom.TableName);
            thisDG.ColumnsEng = new List<string>();

            foreach (DataColumn i in TableFrom.Columns)
            {
                TableTo.Columns.Add(DictionarySearch(ColumnsDictionary, i.ColumnName));
                thisDG.ColumnsEng.Add(i.ColumnName);
            }
            foreach(DataRow i in TableFrom.Rows)
            {
                TableTo.Rows.Add(i.ItemArray);
            }
            return TableTo;
        }
        public static string DictionarySearch(Dictionary<string, string> DictionaryFrom, string key)
        {
            string toReturn = "error";
            for (int i = 0; i < DictionaryFrom.Keys.Count; i++)
            {
                if (DictionaryFrom.Keys.ElementAt(i) == key)
                {
                    toReturn = DictionaryFrom.Values.ElementAt(i);
                    break;
                }
            }
            return toReturn;
        }


        public static void ApplicationStop()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
