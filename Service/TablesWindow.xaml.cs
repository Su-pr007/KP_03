using MySql.Data.MySqlClient;
using Service.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml;
using System.Xml.Linq;
using static Service. Variables;

namespace Service
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class TablesWindow : Window
	{
		int SelectedTabIndex;
		object SelectedTabName;


		public TablesWindow()
		{
			InitializeComponent();
			
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			ApplicationStop();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string[] TabHeaders;
			switch (ProfileId)
			{
				// ServiceManager - Менеджер
				case 1:
					TabHeaders = new string[] { "Заказы", "Сотрудники" };
					break;
				// ServiceRepairer - Ремонтник
				case 2:
					TabHeaders = new string[] { "Заказы", "Запчасти", "Ремонтируемые модели" };
					break;
				// ServiceAccountant - Бухгалтер
				case 3:
					TabHeaders = new string[] {  };
					break;
				// ServicePersDepart - Отдел кадров
				case 4:
					TabHeaders = new string[] { "Сотрудники" };
					break;
				// ServiceDBAdmin - Администратор БД
				case 5:
					TabHeaders = new string[] { "*" };
					break;
				// ServiceOrderer - Заказчик
				case 6:
					TabHeaders = new string[] { "Заказы" };
					break;
				// Другие
				default:
					TabHeaders = new string[] { };
					break;
			}
			TabItems.Clear(this);
			TabItems.Create(this, TabHeaders);

			/*string sql = "SELECT e_id 'Id', e_surname 'Фамилия', e_name_patronymic 'Имя, отчество', e_age 'Возраст', e_sex 'Пол', e_passport_series_and_number 'Серийный номер паспорта', p_name 'Должность' FROM employees INNER JOIN positions ON employees.e_p_id = positions.p_id";*/



			// -----------------------------------------------
			// Здесь выбор того что будет добавлено в таблицы

			for (int i = 0; i < MainTabs.Items.Count; i++)
			{
				DataGrid CurrentDataGrid = DataGrids.ElementAt(i);
				switch (CurrentDataGrid.Name.ToString())
				{
					case "Employees":
						FillTable.ByDG("SELECT * FROM employees", CurrentDataGrid);
						break;
					case "Orders":
						FillTable.ByDG("SELECT * FROM orders", CurrentDataGrid);
						break;
					case "Fault_Types":
						FillTable.ByDG("SELECT * FROM fault_types", CurrentDataGrid);
						break;
					case "Parts":
						FillTable.ByDG("SELECT * FROM parts", CurrentDataGrid);
						break;
					case "Positions":
						FillTable.ByDG("SELECT * FROM positions", CurrentDataGrid);
						break;
					case "Repaired_Models":
						FillTable.ByDG("SELECT * FROM repaired_models", CurrentDataGrid);
						break;
					case "Served_Shops":
						FillTable.ByDG("SELECT * FROM served_shops", CurrentDataGrid);
						break;
					case null:
						Console.WriteLine("НУЛЛЫ");
						break;
					default:

						break;
				}
			}

		}


		private void ReturnButton_Click(object sender, RoutedEventArgs e)
		{
			MenuWindow_Window.Show();
			this.Hide();
		}

		private void FilterButton_Click(object sender, RoutedEventArgs e)
		{

			// POTOM

			/*try
			{
				IEnumerable<XElement> asdasd = root.Elements("TabItem");


				IEnumerable<XElement> address = from el in root.Elements("TabItem") where (string)el.Attribute("Name") == "TabItem0" select el;
				foreach (XElement el in address) Console.WriteLine(el);
				Console.WriteLine("ara ara");
			}
			catch
			{*/
				Notification.ShowError("Еррор");
			/*}*/
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{   

		}


		private void TabControlChangedSelection(object sender, SelectionChangedEventArgs e)
		{
			// Индекс выбранной вкладки
			int SelectedIndex = MainTabs.SelectedIndex;

			SelectedTabIndex = SelectedIndex;
			SelectedTabName = MainTabs.Items.GetItemAt(SelectedIndex);
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ChangeRowButton_Click(object sender, RoutedEventArgs e)
		{
			DataGrid SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);
			IList<DataGridCellInfo> SelectedCells = SelectedDataGrid.SelectedCells;
			if (SelectedCells.Count == SelectedDataGrid.Columns.Count)
			{
				new AddingChangingWindow(SelectedDataGrid, SelectedCells).Show();
			}
			else
			{
				Notification.ShowNotice("Выберите одну строку");
			}
		}
		public string SearchTable()
		{
			string search = "DataGridOnTab" + SelectedTabIndex.ToString();
			Console.WriteLine(search);
			foreach (XElement i in new XElement("TablesWindow.xaml").Elements("DataGrid"))
			{
				Console.WriteLine(i.Value);
				if (i.Attribute("Name").Value == search)
				{
					return search;
				}
			}
			return null;
		}

		// Добавить строку
		private void AddRowButton_Click(object sender, RoutedEventArgs e)
		{
			DataGrid SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);
			new AddingChangingWindow(SelectedDataGrid).Show();
		}
	}
}
