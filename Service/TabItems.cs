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
        // Count - Количество вкладок
        public static void Create(TablesWindow Window, string[] TabHeaders)
        {
            if (TabHeaders.Length > 0)
            {
                int TabCount = TabHeaders.Length;

                Brush ColorToBrush = new SolidColorBrush(new Color()
                {
                    R = 229,
                    G = 229,
                    B = 229,
                    A = 255,
                });

                for (int i = 0; i < TabCount; i++)
                {
                    string Name = "DataGridOnTab" + i.ToString();
                    DataGrid CurrentDataGrid = new DataGrid()
                    {
                        Name = Name,
                        Margin = new Thickness
                        {
                            Left = 10,
                            Top = 10,
                            Right = 150,
                            Bottom = 10,
                        },
                        MinWidth = 600,
                        MinHeight = 350,
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
                        Header = TabHeaders.ElementAt(i),
                        Name = "TabItem" + i.ToString(),
                    };

                    CurrentGrid.Children.Add(CurrentDataGrid);
                    CurrentTabItem.Content = CurrentGrid;
                
                    Window.MainTabs.Items.Add(CurrentTabItem);
                }
                
            }
        }




    }
}
