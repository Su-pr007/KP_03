using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace Service
{
	/// <summary>
	/// Логика взаимодействия для FilterWindow.xaml
	/// </summary>
	/// 
	public partial class FilterWindow : Window
	{
		string ThisDGName;
		public DataTable FilterDataTable;
		string ThisWindowName;

		public FilterWindow(DataGrid ThisDG, Window window)
		{
			InitializeComponent();

			ThisWindowName = window.Name;
			ThisDGName = ThisDG.Name;
		}
		private void FilterWindow1_Loaded(object sender, RoutedEventArgs e)
		{
			FilterDataTable = null;
			AndCheckBox.IsChecked = Variables.AndFilterChecked;
			DataTable thisDV;
			if (ThisWindowName == "TablesWindow") thisDV = Variables.FindMyDGByName(ThisDGName).DV.Table;
			else thisDV = Variables.FindMyDGByName(ThisDGName).DV.Table;


			DataTable NewTable = new DataTable();
			for (int i = 0; i < 2; i++)
			{
				DataColumn NewColumn = new DataColumn()
				{
					ColumnName = i == 0 ? "Поле" : "Значение",
					ReadOnly = i == 0,
				};
				NewTable.Columns.Add(NewColumn);
			}
			for(int i = 0; i < thisDV.Columns.Count; i++)
			{
				DataRow NewRow = NewTable.NewRow();
				NewRow[0] = thisDV.Columns[i].ColumnName;
				NewRow[1] = "";
				NewTable.Rows.Add(NewRow);
			}

			FilterDataTable = NewTable;
			FilterDataGrid.ItemsSource = NewTable.DefaultView;
		}


		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			string msg = "Поиск завершён";
			Variables.AndFilterChecked = AndCheckBox.IsChecked.Value;

			DataTable DGTable = Variables.FindMyDGByName(Variables.CurrentDataGridName).DV.Table;

			DataTable FilteredDT = new DataTable();
			List<string> FilterDGData = new List<string>();


			for (int i = 0; i < FilterDataTable.Rows.Count; i++)
			{
				FilteredDT.Columns.Add(DGTable.Columns[i].ColumnName);
				FilterDGData.Add(FilterDataTable.Rows[i][1].ToString());
			}
			bool FoundRow = false;


			for (int i = 0; i < FilterDGData.Count; i++)
			{
				if (FilterDGData[i] != "")
				{
					FoundRow = true;
					break;
				}
			}
			if (FoundRow)
			{
				// Фильтрация "Или"
				if (!AndCheckBox.IsChecked.Value)
				{
					for (int i = 0; i < DGTable.Rows.Count; i++)
					{
						for (int j = 0; j < DGTable.Columns.Count; j++)
						{
							if (FilterDGData[j] == "") continue;
							Regex regex;
							try
							{
								regex = new Regex(@"" + FilterDGData[j]);
							}
							catch
							{
								msg = "Введён недопустимый символ";
								break;
							}

							if (regex.IsMatch(DGTable.Rows[i].ItemArray[j].ToString()))
							{
								DataRow NewRow = FilteredDT.NewRow();
								NewRow.ItemArray = DGTable.Rows[i].ItemArray;
								FilteredDT.Rows.Add(NewRow);
								break;
							}
						}
					}

				}
				// Фильтрация "И"
				else
				{
					Dictionary<int, string> q = new Dictionary<int, string>();
					for (int i = 0; i < FilterDGData.Count; i++) if (FilterDGData[i] != "") q.Add(i, FilterDGData[i]);

					DataTable CurrentDT = Variables.FindMyDGByName(Variables.CurrentDataGridName).DV.Table;

					foreach (var FilterRow in q)
					{
						for (int j = 0; j < CurrentDT.Rows.Count; j++)
						{
							for (int i = 0; i < CurrentDT.Rows.Count; i++)
							{
								Regex regex;

								try
								{
									regex = new Regex(@"" + FilterRow.Value);
								}
								catch
								{
									msg = "Введён недопустимый символ";
									break;
								}
								if (!regex.IsMatch(CurrentDT.Rows[i].ItemArray[FilterRow.Key].ToString()))
								{
									CurrentDT.Rows.RemoveAt(i);
									break;
								}
							}

						}

					}

					for (int i = 0; i < CurrentDT.Rows.Count; i++)
					{
						FilteredDT.Rows.Add(CurrentDT.Rows[i].ItemArray);
					}
				}

				Variables.FindMyDGByName(Variables.CurrentDataGridName).DV = FilteredDT.DefaultView;
				Variables.FindMyDGByName(Variables.CurrentDataGridName).DG.ItemsSource = FilteredDT.DefaultView;
			}
			else
			{
				Variables.TablesWindow_Window.ReloadTables();
			}
			Notification.ShowNotice(msg);


			Hide();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Hide();
		}

		private void FilterWindow1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape) Hide();
		}
	}
}
