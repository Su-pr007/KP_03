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
		string thisDGName;
		public DataTable FilterDataTable;

		public FilterWindow(DataGrid thisDG)
		{
			InitializeComponent();


			thisDGName = thisDG.Name;
		}
		private void FilterWindow1_Loaded(object sender, RoutedEventArgs e)
		{
			AndCheckBox.IsChecked = Variables.AndFilterChecked;
			MyDataGrid thisDG = Variables.FindMyDGByName(thisDGName);
			DataTable thisTV = thisDG.TV.Table;


			DataTable NewTable = new DataTable();
			for (int i = 0; i < 2; i++)
			{
				DataColumn NewColumn = new DataColumn()
				{
					ColumnName = i == 0 ? "Поле" : "Значение",
					ReadOnly = i == 0 ? true : false,
				};
				NewTable.Columns.Add(NewColumn);
			}
			for(int i = 0; i < thisTV.Columns.Count; i++)
			{
				DataRow NewRow = NewTable.NewRow();
				NewRow[0] = thisTV.Columns[i].ColumnName;
				NewRow[1] = "";
				NewTable.Rows.Add(NewRow);
			}

			FilterDataTable = NewTable;
			FilterDataGrid.ItemsSource = NewTable.DefaultView;
		}


		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Variables.AndFilterChecked = AndCheckBox.IsChecked.Value;


			MyDataGrid MyDG = Variables.FindMyDGByName(Variables.CurrentDataGridName);
			DataTable DGTable = MyDG.TV.Table;

			DataTable FilteredDT = new DataTable();
			List<string> FilterDGData = new List<string>();


			for (int i = 0; i < FilterDataTable.Rows.Count; i++)
			{
				FilteredDT.Columns.Add(Variables.FindMyDGByName(Variables.CurrentDataGridName).TV.Table.Columns[i].ColumnName);
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
							Regex regex = new Regex(@"" + FilterDGData[j]);

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

					DataTable CurrentDT = Variables.FindMyDGByName(Variables.CurrentDataGridName).TV.Table;

					foreach (var FilterRow in q)
					{
						for(int j = 0; j < CurrentDT.Rows.Count; j++)
                        {
							for (int i = 0; i < CurrentDT.Rows.Count; i++)
							{
								Regex regex = new Regex(@"" + FilterRow.Value);
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


				MyDG.DG.ItemsSource = FilteredDT.DefaultView;
			}
			else Variables.TablesWindow_Window.ReloadTables();


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
