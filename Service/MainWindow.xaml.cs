using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TabItems.Create(this, 3);

            string sql = "SELECT e_id 'Id', e_surname 'Фамилия', e_name_patronymic 'Имя, отчество', e_age 'Возраст', e_sex 'Пол', e_passport_series_and_number 'Серийный номер паспорта', p_name 'Должность' FROM employees INNER JOIN positions ON employees.e_p_id = positions.p_id";

            FillTable.ByQuery(sql, DataGridEmpRep);






            // ================= ПЕЧАТЬ ВОСЬМИ ТРИГРАМ. ВНУТРИ ДЕВЯТИХВОСТЫЙ =========================
            /*
                        // сокрытие вкладки
                        /*MainTabs.Items.Remove(MainTabItem);*//*
                        string[] TabNames;
                        string[] TabHeaders;
                        // списки вкладок для каждого пользователя
                        switch (ProfileId)
                        {
                            // ServiceManager - Менеджер
                            case 1:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // ServiceRepairer - Ремонтник
                            case 2:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // ServiceAccountant - Бухгалтер
                            case 3:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // ServicePersDepart - Отдел кадров
                            case 4:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // ServiceDBAdmin - Администратор БД
                            case 5:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // ServiceOrderer - Заказчик
                            case 6:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                            // Другие
                            default:
                                TabHeaders = new string[] { "Отдел кадров", "test1", "test2" };
                                break;
                        }






                        // Создание массива TabNames
                        TabNames = new string[TabHeaders.Length];
                        for(int i = 0; i < TabHeaders.Length; i++)
                        {
                            TabNames[i] = "Tab" + Convert.ToString(i);
                        }

                        // Добавление на окно
                        for (int i = 0; i < TabNames.Length; i++)
                        {
                            TabItem CurrentTab = new TabItem
                            {
                                Name = TabNames[i],
                                Header = TabHeaders[i],
                            };
                            // Создание цвета для Grid
                            Color GridColor = Color.FromRgb(50, 50, 50);
                            Grid grid = new Grid
                            {
                                Tag = "Grid" + Convert.ToString(i),
                                Background = new SolidColorBrush(GridColor),

                                *//*Background = Drawing.SystemBrush(System.Drawing.Color.Red),*//*
                            };

                            DataGrid Table = new DataGrid();

                            CurrentTab.Content = grid;

                            switch (TabHeaders[i])
                            {
                                case "Отдел кадров":
                                    // ДОБАВИТЬ ЧТО-НИБУДЬ

                                    break;
                                // И СЮДА

                                default:
                                    // СЮДА ТОЖЕ

                                    break;
                            }
                            CurrentTab.Content = grid.Children.Add(Table);
                            MainTabs.Items.Add(CurrentTab);
                        }


                        // Добавление на вкладки таблиц
            */

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Variables.Clear();
            this.Hide();
            new LoginWindow().Show();
        }

    }
}
