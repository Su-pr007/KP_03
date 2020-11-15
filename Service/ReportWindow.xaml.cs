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
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void ReportWindow_Closed(object sender, EventArgs e)
        {
            ApplicationStop();
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT e_id 'Id', e_surname 'Фамилия', e_name_patronymic 'Имя, отчество', e_age 'Возраст', e_sex 'Пол', e_passport_series_and_number 'Серийный номер паспорта', p_name 'Должность' FROM employees INNER JOIN positions ON employees.e_p_id = positions.p_id";

            FillTable.ByDG(sql, DataGridEmpRep);

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

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
