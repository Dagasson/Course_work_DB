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
using System.Data.SqlClient;
//using Course_work.Classes;

namespace Course_work
{
    /// <summary>
    /// Логика взаимодействия для User_window.xaml
    /// </summary>
    public partial class User_window : Window
    {
        public User_window()
        {
            InitializeComponent();
            setUserInfo();
            setShopInfo();
        }

        private void setUserInfo()
        {
            Current_Login.Text = User_Model.Login;
            Current_Balance.Text = User_Model.Balance.ToString() + " тугриков.";
            if (User_Model.isAdmin == 2) Current_isAdmin.Text = "Пользователь является администратором.";
            else Current_isAdmin.Text = "Пользователь не является администратором.";

        }

        private void setShopInfo()
        {
            SqlConnection connection = new SqlConnection(Connect_data.currentConnect);

            SqlCommand command = new SqlCommand("usp_ShopSelectAll", connection);//указываем какую команду вызываем
            command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
            connection.Open();//открываем соединение
                              //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                              //добавляем параметр в вызываемую процедуру
                              //command.Parameters.AddWithValue("@Login", Login);
                              //Не добавляем.

            SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
            List<Classes.Shop_Model> Shop = new List<Classes.Shop_Model>();
             
                while (result.Read())//Пока есть что читать
                {
                    Classes.Shop_Model Shop_Info = new Classes.Shop_Model(
                        int.Parse(result["id"].ToString()),
                           result["Category"].ToString(),
                           result["Name"].ToString(),
                           double.Parse(result["Cost"].ToString()),
                           result["onStorage"].ToString(),
                           DateTime.Parse(result["DateOfIncoming"].ToString()),
                           int.Parse(result["hmOnStorage"].ToString()),
                            result["info"].ToString()                           
                        );
                    Shop.Add(Shop_Info);
                }
            
            connection.Close();
            Shop_list.ItemsSource = Shop;

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
