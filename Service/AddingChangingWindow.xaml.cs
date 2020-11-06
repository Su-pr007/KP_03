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

		IList<DataGridCellInfo> SelectedCells;
		public DataGrid SelectedDataGrid;
		List<string> Columns;

		bool IsChange;

		public void InitVariables()
		{
			SelectedCells = null;
			SelectedDataGrid = null;
			Columns = new List<string>();


		}

		// ChangingWindow
		public AddingChangingWindow(DataGrid SelectedDataGrid, IList<DataGridCellInfo> SelectedCells)
		{
			Owner = Variables.TablesWindow_Window;
			InitializeComponent();

			InitVariables();
			IsChange = true;

			this.SelectedDataGrid = SelectedDataGrid;
			this.SelectedCells = SelectedCells;
		}

		// Adding Window	
		public AddingChangingWindow(DataGrid SelectedDataGrid)
		{
			Owner = Variables.TablesWindow_Window;
			InitializeComponent();

			InitVariables();
			IsChange = false;

			this.SelectedDataGrid = SelectedDataGrid;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Убрать
			Notification.ShowError("ЕРРОРЫ");
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
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
				// ---------------------------------------------
				// Вставить ссылку на содержимое полей
				if (IsChange) xsw.Add(SelectedCells.ElementAt(i).ToString());
				else xsw.Add("");
			}

			ACDataGrid.ItemsSource = CreateDataTable(xsw).DefaultView;


		}
		// Вставить только значения. Названия полей функция берёт из переменной Columns
		public DataTable CreateDataTable(List<object> Values)
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
	}
}
