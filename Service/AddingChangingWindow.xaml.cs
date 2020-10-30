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

namespace Service.Properties
{
    /// <summary>
    /// Логика взаимодействия для AddingChangingWindow.xaml
    /// </summary>
    public partial class AddingChangingWindow : Window
    {
        string TableName;
        public AddingChangingWindow(string TableName)
        {
            this.TableName = TableName;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Notification.ShowError("ЕРРОРЫ");
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddingChangingWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            label1test.Content = TableName + ": ";

        }
    }
}
