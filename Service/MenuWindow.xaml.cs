using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using MySql;
using static Service.Variables;
using System.Configuration;

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Скрыть все кнопки
            ToTablesButton.Visibility = Visibility.Collapsed;
            ToQueriesButton.Visibility = Visibility.Visible;
            CreateOrderButton.Visibility = Visibility.Collapsed;
            // Показать каждому свои
            switch (ProfileId)
            {
                // Менеджер
                case 1:
                    ToTablesButton.Visibility = Visibility.Visible;
                    break;
                // Ремонтник
                case 2:
                    ToTablesButton.Visibility = Visibility.Visible;
                    break;

                //  Бухгалтер
                case 3:
                    break;
                //  Отдел кадров
                case 4:
                    ToTablesButton.Visibility = Visibility.Visible;
                    break;
                //  Админ базы данных
                case 5:
                    ToTablesButton.Visibility = Visibility.Visible;
                    CreateOrderButton.Visibility = Visibility.Visible;
                    break;
                //  Заказчик
                case 6:
                    CreateOrderButton.Visibility = Visibility.Visible;
                    ToQueriesButton.Visibility = Visibility.Collapsed;
                    break;
            }
            if (ProfileId >= 6)
            {
                LoginTextBlock.Text = "Клиент";
            }
            else
            {
                LoginTextBlock.Text = new serviceDataSetTableAdapters.positionsTableAdapter().GetData().ElementAt(ProfileId-1).p_name;
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ApplicationStop();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            LoginWindow_Window.Show();
            ClearVariables();
        }

        private void ToTablesButton_Click(object sender, RoutedEventArgs e)
        {
            TablesWindow_Window.Show();
            this.Hide();
        }

        private void ToQueriesButton_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow_Window.Show();
            this.Hide();
        }


        // УБРАТЬ
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    ToTablesButton_Click(new object(), new RoutedEventArgs());
                    break;
                case Key.D2:
                    ToQueriesButton_Click(new object(), new RoutedEventArgs());
                    break;
                case Key.D3:
                    CreateOrderButton_Click(new object(), new RoutedEventArgs());
                    break;
                case Key.Back:
                    ReturnButton_Click(new object(), new RoutedEventArgs());
                    break;
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateOrderWindow().Show();
            Hide();
        }
    }
}
