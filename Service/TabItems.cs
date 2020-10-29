using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void Create(TablesWindow Window, int Count)
        {

            Brush ColorToBrush = new SolidColorBrush(new Color()
            {
                R = 229,
                G = 229,
                B = 229,
                A = 255,
            });

            for (int i = 0; i < Count; i++)
            {
                DataGrid CurrentDataGrid = new DataGrid()
                {
                    Name = "DataGridOnTab" + i.ToString(),
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

                Grid CurrentGrid = new Grid()
                {
                    Background = ColorToBrush,
                };

                TabItem CurrentTabItem = new TabItem()
                {
                    Header = "TabItem" + i.ToString(),
                    Name = "TabItem" + i.ToString(),
                };

                CurrentGrid.Children.Add(CurrentDataGrid);
                CurrentTabItem.Content = CurrentGrid;
                
                Window.MainTabs.Items.Add(CurrentTabItem);
            }
        }



    }
}
