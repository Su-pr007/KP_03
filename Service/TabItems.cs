using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Service
{
    class TabItems
    {

        // Создать вкладки
        // ------------------
        // Window - Используется для передачи текущего окна. Использовать this
        // TabHeaders - Названия вкладок
        public static void Create(TablesWindow Window, string[] TabHeaders)
        {
            string[] AllTabHeaders = new string[] { "Сотрудники", "Заказы", "Запчасти", "Должности", "Ремонтируемые модели", "Обслуживаемые магазины", "Виды неисправностей", };
            string[] AllTabNames = new string[] { "employees", "orders", "parts", "positions", "repaired_models", "served_shops", "fault_types", };

            if (TabHeaders.Length == 1 && TabHeaders.First() == "*")
            {
                TabHeaders = AllTabNames;
            }

            int TabCount = TabHeaders.Length;


            if (TabCount > 0)
            {

                Brush ColorToBrush = new SolidColorBrush(new Color()
                {
                    R = 229,
                    G = 229,
                    B = 229,
                    A = 255,
                });

                for (int i = 0; i < TabCount; i++)
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
                    Variables.TablesWindow_Window.RegisterName(CurrentDataGrid.Name, CurrentDataGrid);
                    Variables.DataGrids.Add(CurrentDataGrid);


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

                    Window.MainTabs.Items.Add(CurrentTabItem);

                    string PKey;
                    switch (AllTabNames[i])
                    {
                        case "employees":
                            PKey = "e_id";
                            break;
                        case "orders":
                            PKey = "o_id";
                            break;
                        case "parts":
                            PKey = "part_id";
                            break;
                        case "positions":
                            PKey = "p_id";
                            break;
                        case "repaired_models":
                            PKey = "rm_id";
                            break;
                        case "served_shops":
                            PKey = "ss_id";
                            break;
                        case "fault_types":
                            PKey = "ft_id";
                            break;
                        case "parts_faults":
                            PKey = "pf_id";
                            break;
                        default:
                            PKey = "error";
                            break;
                    }


                    Variables.MyDGs.Add(new MyDataGrid());

                    CurrentDataGrid.MouseDoubleClick += CurrentDataGrid_MouseDoubleClick;

                    Variables.MyDGs[i].DG = CurrentDataGrid;
                    Variables.MyDGs[i].PK = PKey;

                }
                
            }
        }

        private static void CurrentDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Variables.TablesWindow_Window.ChangeRowButton_Click(new object(), new RoutedEventArgs());
        }

        public static void Clear(TablesWindow thisWindow)
        {
            thisWindow.MainTabs.Items.Clear();
        }




    }
}
