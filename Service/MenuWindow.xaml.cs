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
using static Service.Variables;

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
            LoginTextBlock.Text = DBlogin;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow_Window.Show();
            ClearVariables();
        }

        private void ToTablesButton_Click(object sender, RoutedEventArgs e)
        {
            TablesWindow_Window.Show();
            this.Hide();
        }

        private void ToQueries_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow_Window.Show();
            this.Hide();
        }
    }
}
