using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using static Service.Variables;

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public int SelectedTabIndex;
        public ReportWindow()
        {
            SelectedTabIndex = 0;
            InitializeComponent();
        }

        private void ReportWindow_Closed(object sender, EventArgs e)
        {
            ApplicationStop();
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
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
            ReportTabs.Items.Clear();






            string[] AllTabHeaders = new string[] { "Отдел кадров", "Список неисправностей", "Список заказов" };
            string[] AllTabNames = new string[] { "PersonnelDepartment", "FaultsList", "OrdersList" };

            if (TabHeaders.Length == 1 && TabHeaders.First() == "*")
            {
                TabHeaders = AllTabNames;
            }



            if (TabHeaders.Length > 0)
            {

                Brush ColorToBrush = new SolidColorBrush(new Color()
                {
                    R = 229,
                    G = 229,
                    B = 229,
                    A = 255,
                });

                for (int i = 0; i < TabHeaders.Length; i++)
                {
                    DataGrid CurrentDataGrid = new DataGrid()
                    {
                        Name = AllTabNames[i],
                        Margin = new Thickness
                        {
                            Left = 0,
                            Top = 0,
                            Right = 0,
                            Bottom = 0,
                        },
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        IsReadOnly = true,
                    };
                    TablesWindow_Window.RegisterName(CurrentDataGrid.Name, CurrentDataGrid);
                    DataGrids.Add(CurrentDataGrid);


                    Grid CurrentGrid = new Grid()
                    {
                        Background = ColorToBrush,
                    };

                    TabItem CurrentTabItem = new TabItem()
                    {
                        IsSelected = i == 0,
                        Header = AllTabHeaders.ElementAt(i),
                        Name = TabHeaders.ElementAt(i),
                    };


                    CurrentGrid.Children.Add(CurrentDataGrid);
                    CurrentTabItem.Content = CurrentGrid;

                    ReportTabs.Items.Add(CurrentTabItem);



                    MyDGs.Add(new MyDataGrid());


                    MyDGs[i].DG = CurrentDataGrid;



                    MyDGs[i].DG.ItemsSource = null;

                    var DataGridName = MyDGs[i].DG;

                    DataTableCollection Tables = ServiceDB.Tables;

                    if (Tables.Contains(DataGridName.Name))
                    {
                        DataView TableView = new DataView();


                        // УБРАТЬ
                        // От сих не готово
                        for (int j = 0; j < Tables.Count; j++)
                        {
                            switch (DataGridName.Name)
                            {
                                case "":
                                    TableView = GetTableDataByTableName("employees");
                                    break;
                                case "fault_types":
                                    TableView = GetTableDataByTableName("fault_types");
                                    break;
                                case "orders":
                                    TableView = GetTableDataByTableName("orders");
                                    break;
                                default:
                                    continue;
                            }
                        }
                        var MyDGName = FindMyDGByName(DataGridName.Name);
                        MyDGName.Name = DataGridName.Name;

                        if (TableView != null)
                        {
                            MyDGName.TV = TableView;
                            DataGridName.ItemsSource = TableView;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Неопознанная таблица");
                    }



                }


            }







        }



        public void ReadSqlQuery(string sql)
        {
            /*conn.Open();
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();

            DataTable NewTable = new DataTable();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                NewTable.Columns.Add(new DataColumn(reader.));
            }
            // читаем результат
            while (reader.Read())
            {
                for(int i = 0; i < reader.FieldCount; i++)
                {
                    reader.GetValue(i);

                }
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();*/
        }



        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow_Window.Show();
            this.Hide();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            new FilterWindow(DataGrids.ElementAt(SelectedTabIndex)).ShowDialog();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            new SearchWindow().ShowDialog();
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Индекс выбранной вкладки
            SelectedTabIndex = ReportTabs.SelectedIndex;
        }
    }
}
