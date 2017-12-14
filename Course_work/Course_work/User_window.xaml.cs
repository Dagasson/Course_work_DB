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
            getCurrentOrders();
        }

        private void setUserInfo()
        {
            Current_Login.Text = User_Model.Login;
            CardsId.Text = User_Model.idOfCards.ToString();
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
                           DateTime.Parse(result["DateOfIncoming"].ToString()),
                           result["info"].ToString()                           
                        );
                    Shop.Add(Shop_Info);
                }
            
            connection.Close();
            Shop_list.ItemsSource = Shop;
            
        }

        private void getCurrentOrders()
        {
            SqlConnection connection = new SqlConnection(Connect_data.currentConnect);

            if (getOrdersByDate.IsChecked == true)
            {
                SqlCommand command = new SqlCommand("usp_SelectCurrentOrders", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@idOfUser", User_Model.id);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
                List<Classes.Order_Model> Orders = new List<Classes.Order_Model>();

                while (result.Read())//Пока есть что читать
                {
                    Classes.Order_Model Order_Info = new Classes.Order_Model(
                        int.Parse(result["id"].ToString()),
                        int.Parse(result["idOfUser"].ToString()),
                        double.Parse(result["Summ"].ToString()),
                        DateTime.Parse(result["dateOfAssembly"].ToString())
                        );
                    Orders.Add(Order_Info);
                }

                connection.Close();

                Orders_list.ItemsSource=null;
                Orders_list.ItemsSource = Orders;
            }
            else if(getOrdersWithoutDate.IsChecked==true)
            {
                SqlCommand command = new SqlCommand("usp_SelectUsersOrders", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@idOfUser", User_Model.id);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
                List<Classes.Order_Model> Orders = new List<Classes.Order_Model>();

                while (result.Read())//Пока есть что читать
                {
                    Classes.Order_Model Order_Info = new Classes.Order_Model(
                        int.Parse(result["id"].ToString()),
                        int.Parse(result["idOfUser"].ToString()),
                        double.Parse(result["Summ"].ToString()),
                        DateTime.Parse(result["dateOfAssembly"].ToString())
                        );
                    Orders.Add(Order_Info);
                }

                connection.Close();

                Orders_list.ItemsSource=null;
                Orders_list.ItemsSource = Orders;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Create_orders cor = new Create_orders();
            cor.Show();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)//Эта хрень важна, я без нее минут 40 ебался, пока не понял её значимость.
        {

        }

        private void getOrdersProduct_Click(object sender, RoutedEventArgs e)
        {
            int idOfOrd;
            if (Int32.TryParse(idOfOrder.Text, out idOfOrd))
            {
                List<Classes.getProduct_Model> prod = new List<Classes.getProduct_Model>();
                SqlConnection connection = new SqlConnection(Connect_data.currentConnect);
                SqlCommand command = new SqlCommand("usp_SelectproductByOrdersId", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру

                connection.Open();//открываем соединение
                command.Parameters.AddWithValue("@idOfOrder", idOfOrd);

                SqlDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    Classes.getProduct_Model product = new Classes.getProduct_Model(
                        result["Name"].ToString(),
                        result["Category"].ToString(),
                        int.Parse(result["amount"].ToString()),
                        DateTime.Parse(result["dateOfIncoming"].ToString())
                    );
                    prod.Add(product);
                }
                connection.Close();

                Orders_list.ItemsSource=null;
                Orders_list.ItemsSource = prod;
                Orders_list.Items.Refresh();
            }

        }

        private void getOrders_Click(object sender, RoutedEventArgs e)
        {
            getCurrentOrders();
        }

        private void sortByCost_Click(object sender, RoutedEventArgs e)
        {
            double from;
            double to;
            if (Double.TryParse(fromSum.Text, out from) && Double.TryParse(toSum.Text, out to)&&from>=0&&to>=0)
            {
                List<Classes.Shop_Model> shop = new List<Classes.Shop_Model>();
                SqlConnection connection = new SqlConnection(Connect_data.currentConnect);
                SqlCommand command = new SqlCommand("usp_getProductByCost", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру

                connection.Open();//открываем соединение
                command.Parameters.AddWithValue("@fromSum", from);
                command.Parameters.AddWithValue("@toSum", to);

                SqlDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    Classes.Shop_Model product = new Classes.Shop_Model(
                           int.Parse(result["id"].ToString()),
                           result["Category"].ToString(),
                           result["Name"].ToString(),
                           double.Parse(result["Cost"].ToString()),
                           DateTime.Parse(result["DateOfIncoming"].ToString()),
                           result["info"].ToString()
                    );
                    shop.Add(product);
                }
                connection.Close();

                Shop_list.ItemsSource = null;
                Shop_list.ItemsSource = shop;
                Shop_list.Items.Refresh();
            }
            else MessageBox.Show("Некорректно задан диапазон");
        }
    }
}

