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
using static Service.Variables;

namespace Service
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class TablesWindow : Window
	{
		public static int SelectedTabIndex;
		public static DataGrid SelectedDataGrid;


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




			// Заполнение таблиц
			ReloadTables();
			DataGrids.ElementAt(0);
		}

		public void ReloadTables()
		{
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

						continue;
				}
			}
		}

		// Кнопка возврата в меню
		private void ReturnButton_Click(object sender, RoutedEventArgs e)
		{
			MenuWindow_Window.Show();
			Hide();
		}
		
		// Кнопка фильтрации строк
		private void FilterButton_Click(object sender, RoutedEventArgs e)
		{
			new FilterWindow(DataGrids.ElementAt(SelectedTabIndex)).ShowDialog();



		}

		// Кнопка поиска по таблице
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			new SearchWindow().ShowDialog();
			
		}


		private void TabControlChangedSelection(object sender, SelectionChangedEventArgs e)
		{
			// Индекс выбранной вкладки
			SelectedTabIndex = MainTabs.SelectedIndex;
			CurrentDataGrid = DataGrids[SelectedTabIndex].Name;
		}

		// Кнопка удаления строки
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			string res = "NO";

			int ItemsCount = DataGrids.ElementAt(SelectedTabIndex).SelectedItems.Count;

			if (ItemsCount == 1)
            {
				res = Notification.ShowAsk("Вы уверены что хотите удалить эту запись?").ToString();
            }
			else if(ItemsCount > 1)
			{
				res = Notification.ShowAsk("Вы уверены что хотите удалить эти записи?").ToString();
			}
            else
            {
				Notification.ShowNotice("Выберите удаляемые строки");
			}
			if (res.ToString().ToLower() == "yes")
			{



				Console.WriteLine("YES");
				MyDataGrid thisDG = FindMyDGByName(DataGrids[SelectedTabIndex].Name);

				DataGrid SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);

				string sql = "DELETE FROM " + DataGrids.ElementAt(SelectedTabIndex).Name + " WHERE ";
				
				
				for(int i = 0; i < ItemsCount; i++)
                {
					sql += MyDGs[SelectedTabIndex].PK + " = '" + thisDG.TV.Table.Rows[(SelectedDataGrid.SelectedIndex) + i][0];
                    sql += i == ItemsCount - 1? "';":"' or ";
				}



				ExecuteSqlQueryNoResults(sql);
				
			}




		}



		// Кнопка изменения строки
		public void ChangeRowButton_Click(object sender, RoutedEventArgs e)
		{
			SelectedDataGrid = DataGrids.ElementAt(SelectedTabIndex);

			System.Collections.IEnumerator DataGridEnumerator = SelectedDataGrid.ItemsSource.GetEnumerator();


			IList<DataGridCellInfo> SelectedCells = SelectedDataGrid.SelectedCells;

			

			if (SelectedCells.Count == SelectedDataGrid.Columns.Count)
			{
				new DataManipulationsWindow(SelectedDataGrid, true).Show();
			}
			else
			{
				Notification.ShowNotice("Выберите одну строку");
			}
		}


		// Кнопка добавления строки
		private void AddRowButton_Click(object sender, RoutedEventArgs e)
		{
			new DataManipulationsWindow(DataGrids.ElementAt(SelectedTabIndex), false).ShowDialog();
		}



		public void ExecuteSqlQueryNoResults(string sql)
        {

			conn = DBUtils.GetDBConnection(DBlogin, DBpassword);
			MySqlCommand command = new MySqlCommand(sql, conn);
			try
			{
				Console.WriteLine("Openning connection...");
				conn.Open();
				Console.WriteLine("Connection successful!");


				Console.WriteLine("Trying to execute sql query...");
				command.ExecuteNonQuery();
                Console.WriteLine("Successfully!");

				ReloadTables();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.Message);
				Notification.ShowError("Ошибка при выполнении команды в базе данных");
			}
			finally
			{
				conn.Close();
			}
		}




        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5) ReloadTables();
        }
    }
}
