using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Service.Properties
{
	/// <summary>
	/// Логика взаимодействия для AddingChangingWindow.xaml
	/// </summary>
	public partial class AddingChangingWindow : Window
	{

		public DataGrid SelectedDataGrid = null;
		int DataGridIndex = 0;
		List<string> Columns = new List<string>();
		DataTable ACDataSource = null;
		object EntityId = null;

		bool IsChange;

		// ChangingWindow
		public AddingChangingWindow(DataGrid SelectedDataGrid, int DataGridIndex)
		{
			Owner = Variables.TablesWindow_Window;
			InitializeComponent();

			IsChange = true;
			Title = "Изменение строки";

			this.SelectedDataGrid = SelectedDataGrid;
			this.DataGridIndex = DataGridIndex;
		}

		// Adding Window	
		public AddingChangingWindow(DataGrid SelectedDataGrid)
		{
			Owner = Variables.TablesWindow_Window;
			InitializeComponent();

			IsChange = false;
			Title = "Добавление строки";

			this.SelectedDataGrid = SelectedDataGrid;
		}

		private void AddingChangingWindow1_Loaded(object sender, RoutedEventArgs e)
		{
            for (int i = 0; i < SelectedDataGrid.Columns.Count; i++)
            {
				Columns.Add(SelectedDataGrid.Columns.ElementAt(i).Header.ToString());
            }

			

			List<object> xsw = new List<object>();
			for (int i = 0; i < Columns.Count; i++)
			{
				if (IsChange)
                {
                    xsw.Add(Variables.FindMyDGByName(SelectedDataGrid.Name).TV.Table.Rows[SelectedDataGrid.SelectedIndex][i]);
                }
                else xsw.Add("");
			}

			ACDataSource = CreateACDataTable(xsw);
			ACDataGrid.ItemsSource = ACDataSource.DefaultView;
			ACDataGrid.CanUserAddRows = false;
			ACDataGrid.CanUserDeleteRows = false;


		}
		// Вставить только значения. Названия полей функция берёт из переменной Columns
		public DataTable CreateACDataTable(List<object> Values)
        {

			DataTable NewTable = new DataTable();

			// Создание структуры таблицы
			for(int i = 0; i < 2; i++)
            {

				DataColumn NewColumn = new DataColumn()
				{
					ColumnName = i == 0 ? "Поле" : "Значение",
					ReadOnly = i == 0 ? true : false,
				};
				NewTable.Columns.Add(NewColumn);
            }

			// Заполнение таблицы
			for (int i = 0; i < Columns.Count; i++)
			{
				DataRow DataTableRow = NewTable.NewRow();
                if (i == 0)
                {
					EntityId = Values[i];
					continue;
                }
				DataTableRow.ItemArray = new object[] { Columns[i], Values[i] };

				NewTable.Rows.Add(DataTableRow);
			}

			return NewTable;

		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			List<string> Values = new List<string>();
			bool IsSaved = false;

			Values.Add((string)EntityId);

			for (int i = 0; i < ACDataGrid.Items.Count; i++)
			{
				Values.Add(ACDataSource.Rows[i].ItemArray[1].ToString());
			}

			MySqlConnection conn = DBUtils.GetDBConnection(Variables.DBlogin, Variables.DBpassword);
			try
			{
				Console.WriteLine("Openning of connection...");
				conn.Open();
				Console.WriteLine("Connection successfull!");
				string sql;

				if (IsChange)
                {
					sql = "UPDATE " + SelectedDataGrid.Name + " SET ";
                }
                else
                {
					sql = "INSERT INTO " + SelectedDataGrid.Name + "(";
				}

				for (int i = 0; i < Columns.Count; i++)
				{
					if (!IsChange && i == 0) continue;
					sql += "`" + Variables.FindMyDGByName(SelectedDataGrid.Name).ColumnsEng[i] + "`";
					if (IsChange)
					{
						sql += " = '" + CheckForDate(Values[i]) + "'";
                    }
					sql += i == Values.Count - 1 ? " " : ", ";
				}
				if (IsChange)
				{
					sql += "WHERE `" + Variables.FindMyDGByName(SelectedDataGrid.Name).PK + "` LIKE '" + Values[0] + "';";
				}
                else {
					sql += ") VALUES (";
					for(int i = 1; i < Values.Count; i++)
                    {
						sql += "'"+CheckForDate(Values[i])+"'";
						sql += i == Values.Count - 1 ? " " : ", ";
					}
					sql += ");";
                }

				MySqlCommand command = new MySqlCommand(sql, conn);
				command.ExecuteScalar();
				IsSaved = true;
			}
			catch(Exception err)
			{
                Console.WriteLine("Connection error!");
                Console.WriteLine(err.Message);
				Notification.ShowError("Не получен ответ от базы данных");
			}
			finally
			{
				conn.Close();
                Console.WriteLine("Connection closed");
                if (IsSaved)
                {
					Close();
                }
			}

		}
		public string CheckForDate(string StringFrom)
        {
			string StringTo = StringFrom;
			if (new Regex(@"^\d\d\.\d\d\.\d\d\d?\d? \d?\d:\d\d:\d\d$").IsMatch(StringFrom))
			{
				string[] DTimeStrings = StringFrom.Split(' ');
				string[] Date = DTimeStrings[0].Split('.');
				StringTo = Date[2] + "-" + Date[1] + "-" + Date[0] + " " + DTimeStrings[1];
			}
			else if (new Regex(@"^\d\d\.\d\d\.\d\d\d?\d?$").IsMatch(StringFrom))
			{
				string[] DTimeStrings = StringFrom.Split(' ');
				string[] Date = DTimeStrings[0].Split('.');
				StringTo = Date[2] + "-" + Date[1] + "-" + Date[0] + " 0:00:00";
			}
			return StringTo;

		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        private void AddingChangingWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			Variables.TablesWindow_Window.ReloadTables();
		}
	}
}
