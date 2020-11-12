using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
				object[] NewRow = new object[] { Columns[i], Values[i] };
				DataTableRow.ItemArray = NewRow;

				NewTable.Rows.Add(DataTableRow);
			}

			return NewTable;

		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			List<string> Values = new List<string>();


			for(int i = 0; i < ACDataGrid.Items.Count; i++)
            {
				Values.Add(ACDataSource.Rows[i].ItemArray[1].ToString());

            }


            Console.WriteLine("123");
			// Убрать
			Notification.ShowError("ЕРРОРЫ");
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}
