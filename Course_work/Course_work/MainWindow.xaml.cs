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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Course_work
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Authorization()
        {
            if (login.Text.Length == 0)
            {
                MessageBox.Show("Введите логин.");
                login.Focus();
            }
            else if (password.Text.Length == 0)
            {
                MessageBox.Show("Введите пароль");
                password.Focus();
            }
            else
            {
                string Login = login.Text;
                string Password = password.Text;
                //q1 = new String(login.Text.Where(c => Char.IsDigit(c)).ToArray());//хуитка для проверки число ли введено, тут не нужно

                // SqlConnectionStringBuilder ConnectionString = new SqlConnectionStringBuilder();//данные о подключении, или гайд, как подключиться к БД, если ты безрукая хуйня.
                //ConnectionString.DataSource = @"ASHENVALE";
                //ConnectionString.InitialCatalog = "Course_work";
                //ConnectionString.IntegratedSecurity = true;
                //ConnectionString.Pooling = true;
                //SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);//создаем подключение

                SqlConnection connection = new SqlConnection(Connect_data.defaultConnect);

                SqlCommand command = new SqlCommand("usp_UserSelectByLogin", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@Login", Login);
                command.Parameters.AddWithValue("@Password", Password);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                if (result.Read())//если прочитало- выведет логин по айдишнику
                {
                    string adm = result["isAdmin"].ToString();
                    if (adm == "1") Connect_data.currentConnect = Connect_data.userConnect;
                    else if (adm == "2") Connect_data.currentConnect = Connect_data.adminConnect;

                    User_Model.Login = Login;
                    User_Model.isAdmin = int.Parse(result["isAdmin"].ToString());//Это ж надо было ещё такую дичь придумать. Парсить в стринг, потом в инт, больной ублюдок.
                    User_Model.Balance = double.Parse(result["Balance"].ToString());
                    User_Model.id = int.Parse(result["id"].ToString());

                    // Close();//закрывает окно
                }
                connection.Close();
                User_window us_w = new User_window();
                us_w.Show();
                Close();

            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {        
               Authorization();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Подключение закрыто...");
            }

            Console.Read();
        }

        private void registry_button_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
