using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        string thisDGName;

        public FilterWindow(DataGrid thisDG)
        {
            InitializeComponent();


            thisDGName = thisDG.Name;
        }
        private void FilterWindow1_Loaded(object sender, RoutedEventArgs e)
        {
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


            FilterDataGrid.ItemsSource = NewTable.DefaultView;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Дописать что-то ЗДЕСЬ


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {


            Close();
        }

    }
}
