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
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {

        DataTable SelectedDataGrid;


        public SearchWindow()
        {
            InitializeComponent();

        }


        private void SearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = Variables.TablesWindow_Window;
            SearchTextBox.Focus();
            SelectedDataGrid = Variables.FindMyDGByName(Variables.CurrentDataGrid).TV.Table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable arr = new DataTable();
            string str = SearchTextBox.Text;
            Regex regex = new Regex(@"\w*?" + str + @"\w*?");


            for (int i = 0; i < SelectedDataGrid.Columns.Count; i++)
            {
                DataColumn Column = new DataColumn();
                Column.ColumnName = SelectedDataGrid.Columns[i].ColumnName;

                arr.Columns.Add(Column);
            }



            for (int i = 0; i < SelectedDataGrid.Rows.Count; i++)
            {
                for(int j = 0; j < SelectedDataGrid.Columns.Count; j++)
                {
                    string CurCell = SelectedDataGrid.Rows[i][j].ToString();
                    if (regex.IsMatch(CurCell))
                    {
                        DataRow thisRow = new DataRow().ItemArray = SelectedDataGrid.Rows[i].ItemArray;
                        arr.Rows.Add(thisRow);
                        // СЮДАА
                    }
                }
            }

            Variables.FindMyDGByName(Variables.CurrentDataGrid).DG.ItemsSource = arr.DefaultView;


            Close();
        }
    }
}
