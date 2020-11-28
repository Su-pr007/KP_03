using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        public CreateOrderWindow()
        {
            InitializeComponent();
        }

        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string OrdererName = OrdererNameTB.Text;
            string ModelType = ModelTypeTB.Text;
            string ModelName = ModelNameTB.Text;
            string FaultDesc = FaultDescTB.Text;
            string SerialNumber = SerialNumberTB.Text;

            

        }

        private void CancelOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Variables.MenuWindow_Window.Show();
            Hide();
        }
    }
}
