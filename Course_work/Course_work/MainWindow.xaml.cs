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

        private void selUs()
        {
           string q1 =login.Text;
                q1 = new String(login.Text.Where(c => Char.IsDigit(c)).ToArray());//хуитка для считывания айдишника с поля

            // SqlConnectionStringBuilder ConnectionString = new SqlConnectionStringBuilder();//данные о подключении, или гайд, как подключиться к БД, если ты безрукая хуйня.
            //ConnectionString.DataSource = @"ASHENVALE";
            //ConnectionString.InitialCatalog = "Course_work";
            //ConnectionString.IntegratedSecurity = true;
            //ConnectionString.Pooling = true;
            //SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);//создаем подключение

           // string defaultString = "Data Source = ASHENVALE; Initial Catalog = Course_work; Integrated Security = true;";//Подключение здорового человека.
            SqlConnection connection = new SqlConnection(Connect_data.adminConnect);

            SqlCommand command = new SqlCommand("usp_UsersSelect", connection);//указываем какую команду вызываем
            command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
            connection.Open();//открываем соединение
            //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
            //добавляем параметр в вызываемую процедуру
            command.Parameters.AddWithValue("@id", q1);
            
            SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
        
            if (result.Read())//если прочитало- выведет логин по айдишнику
            {
                String q2 = result["Login"].ToString();
                MessageBox.Show("Login: "+ q2);
               // Close();//закрывает окно
            }
            connection.Close();
            
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {        
               selUs();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               // MessageBox.Show("Подключение закрыто...");
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
