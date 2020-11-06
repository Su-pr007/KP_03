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
            string[] AllTabNames = new string[] { "Employees", "Orders", "Parts", "Positions", "Repaired_Models", "Served_Shops", "Fault_Types" };

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
                        /*Margin = new Thickness
                        {
                            Left = 10,
                            Top = 10,
                            Right = 150,
                            Bottom = 10,
                        },*/
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
                }
                
            }
        }

        public static void Clear(TablesWindow Window)
        {
            Window.MainTabs.Items.Clear();
        }




    }
}
