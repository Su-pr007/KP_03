using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using static Service.Variables;

namespace Service
{
    /// <summary>
    /// Логика взаимодействия для TablesWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TryOpenTablesWindow(LoginTextBox.Text, PasswordBox.Password);
        }

        public void TryOpenTablesWindow(string login, string password)
        {
            bool isConnected = false;
            int ProfileId = 0;
            // Подключение к бд
            MySqlConnection conn = DBUtils.GetDBConnection(login, password);

            // Попытка открытия соединения с базой
            try
            {
                Console.WriteLine("Openning of connection...");
                conn.Open();
                Console.WriteLine("Connection successfull!");
                ProfileId = EmployeePosId(login);
                if (login == "root" && password == "root")
                {
                    Notification.ShowError("Не ври, ты не рут. Я знаю", "Ошибка");
                }
                else if (ProfileId == 0)
                {
                    Notification.ShowError("Неверный логин или пароль.", "Ошибка");
                }
                else
                {
                    InitVariables();
                    isConnected = true;
                }
            }
            catch (Exception err)
            {
                string msg = err.Message;
                Console.WriteLine("Error: " + msg);
                Console.WriteLine("=======================");
                string msgFW = msg.Split(' ').First();
                switch (msgFW)
                {
                    case "Authentication":
                        Notification.ShowError("Неверный логин или пароль", "Ошибка");
                        break;
                    default:
                        Notification.ShowError(err.Message, "Ошибка");
                        break;

                }


            }
            finally
            {
                conn.Close();
                conn.Dispose();

                DBlogin = login;
                DBpassword = password;
            }
            if (isConnected)
            {
                Variables.ProfileId = ProfileId;
                MenuWindow_Window.Show();
                this.Hide();
            }
        }

        private static int EmployeePosId(string login)
        {
            int ProfileId;
            switch (login)
            {
                case "ServiceManager":
                    ProfileId = 1;
                    break;
                case "ServiceRepairer":
                    ProfileId = 2;
                    break;
                case "ServiceAccountant":
                    ProfileId = 3;
                    break;
                case "ServicePersDepart":
                    ProfileId = 4;
                    break;
                case "ServiceDBAdmin":
                    ProfileId = 5;
                    break;
                case "ServiceOrderer":
                    return 6;
                default:
                    Console.WriteLine("what?");
                    ProfileId = 0;
                    break;
            }
            return ProfileId;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TryOpenTablesWindow(LoginTextBox.Text, PasswordBox.Password);
            }
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Focus();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            TryOpenTablesWindow("ServiceOrderer", "12345");
        }

        private void LoginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    PasswordBox.Focus();
                    break;
                case Key.F3:
                    GuestButton_Click(new object(), new RoutedEventArgs());
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ApplicationStop();
        }
    }
}