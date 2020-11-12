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
using System.Windows.Controls.Primitives;
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
		public static int SelectedTabIndex;
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
					TabHeaders = new string[] { };
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




			// -----------------------------------------------
			// Здесь выбор того что будет добавлено в таблицы

			for (int i = 0; i < MainTabs.Items.Count; i++)
			{
				DataGrid CurrentDataGrid = DataGrids.ElementAt(i);
				switch (CurrentDataGrid.Name.ToString())
				{
					case "employees":
						FillTable.ByDG("SELECT * FROM employees", CurrentDataGrid);
						break;
					case "orders":
						FillTable.ByDG("SELECT * FROM orders", CurrentDataGrid);
						break;
					case "fault_types":
						FillTable.ByDG("SELECT * FROM fault_types", CurrentDataGrid);
						break;
					case "parts":
						FillTable.ByDG("SELECT * FROM parts", CurrentDataGrid);
						break;
					case "positions":
						FillTable.ByDG("SELECT * FROM positions", CurrentDataGrid);
						break;
					case "repaired_models":
						FillTable.ByDG("SELECT * FROM repaired_models", CurrentDataGrid);
						break;
					case "served_shops":
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


		// Кнопка возврата в меню
		private void ReturnButton_Click(object sender, RoutedEventArgs e)
		{
			MenuWindow_Window.Show();
			this.Hide();
		}
		
		// Кнопка фильтрации строк
		private void FilterButton_Click(object sender, RoutedEventArgs e)
		{


			Notification.ShowError("Еррор");
		}

		// Кнопка поиска по таблице
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

		// Кнопка удаления строки
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{

		}



		// Кнопка изменения строки
		private void ChangeRowButton_Click(object sender, RoutedEventArgs e)
		{
			DataGrid SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);

			System.Collections.IEnumerator DataGridEnumerator = SelectedDataGrid.ItemsSource.GetEnumerator();


			IList<DataGridCellInfo> SelectedCells = SelectedDataGrid.SelectedCells;
			/*DataGridEnumerator.MoveNext.*/

			



			if (SelectedCells.Count == SelectedDataGrid.Columns.Count)
			{
				new AddingChangingWindow(SelectedDataGrid, SelectedTabIndex).Show();
			}
			else
			{
				Notification.ShowNotice("Выберите одну строку");
			}
		}
		public void SearchTable()
		{



		}

		// Кнопка добавления строки
		private void AddRowButton_Click(object sender, RoutedEventArgs e)
		{
			DataGrid SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);
			new AddingChangingWindow(SelectedDataGrid).Show();
		}
	}
}
