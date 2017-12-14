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

namespace Course_work
{
    /// <summary>
    /// Логика взаимодействия для Admin_window.xaml
    /// </summary>
    public partial class Admin_window : Window
    {
        public Admin_window()
        {
            try
            {
                InitializeComponent();
                showShipper();
                showUsers();
                showDelivery();
                showShop();
            }
            catch(SqlException q)
            {
                MessageBox.Show(q.Message);
            }
            }

        private void addShipper_Click(object sender, RoutedEventArgs e)
        {
            string ShipName = ShippersName.Text;
            int numOfCard;
            if (Int32.TryParse(NumOfCard.Text, out numOfCard))
            {
                SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                SqlCommand command = new SqlCommand("usp_ShipperInsert", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@Name", ShipName);
                command.Parameters.AddWithValue("@numberOfCards", numOfCard);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
                Classes.Shipper_Model.Shippers.Clear();
                connection.Close();
                showShipper();
            }
            else MessageBox.Show("Невалидный номер карты.");
                        
        }

        private void showShipper()
        {
            SqlConnection connection = new SqlConnection(Connect_data.currentConnect);

            SqlCommand command = new SqlCommand("usp_ShipperSelectAll", connection);//указываем какую команду вызываем
            command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
            connection.Open();//открываем соединение
                              //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                              //добавляем параметр в вызываемую процедуру
                              //command.Parameters.AddWithValue("@Login", Login);
                              //Не добавляем.

            SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
            
            while (result.Read())//Пока есть что читать
            {
                Classes.Shipper_Model Shipper_Info = new Classes.Shipper_Model(
                    int.Parse(result["id"].ToString()),
                       result["Name"].ToString(),
                       int.Parse(result["numberOfCards"].ToString())
                       );
                Classes.Shipper_Model.Shippers.Add(Shipper_Info);
            }

            connection.Close();
            Shipper_List.ItemsSource = Classes.Shipper_Model.Shippers;
            Shipper_List.Items.Refresh();
        }

        private void delShipper_Click(object sender, RoutedEventArgs e)
        {
            int idOfShipper;
            if (Int32.TryParse(shippersId.Text, out idOfShipper))
            {
                SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                SqlCommand command = new SqlCommand("usp_ShipperDelete", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@id", idOfShipper);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                Classes.Shipper_Model.Shippers.Clear();
                connection.Close();
                showShipper();
            }
            else MessageBox.Show("Невалидный id.");
            
        }

        private void setAdmin_Click(object sender, RoutedEventArgs e)
        {
            int idOfUs;
            if (Int32.TryParse(idOfUser.Text, out idOfUs))
            {
                SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                SqlCommand command = new SqlCommand("usp_UsersIsAdminUpdate", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@id", idOfUs);
                command.Parameters.AddWithValue("@isAdmin", 2);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                Classes.AllUser_Model.AllUsers.Clear();
                connection.Close();
                showUsers();
            }
            else MessageBox.Show("Невалидный id.");

        }

        private void setUser_Click(object sender, RoutedEventArgs e)
        {
            int idOfUs;
            if (Int32.TryParse(idOfUser.Text, out idOfUs))
            {
                SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                SqlCommand command = new SqlCommand("usp_UsersIsAdminUpdate", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@id", idOfUs);
                command.Parameters.AddWithValue("@isAdmin", 1);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                Classes.AllUser_Model.AllUsers.Clear();
                connection.Close();
                showUsers();
            }
            else MessageBox.Show("Невалидный id.");
        }

        private void showUsers()
        {
            SqlConnection connection = new SqlConnection(Connect_data.currentConnect);

            SqlCommand command = new SqlCommand("usp_UsersSelectAll", connection);//указываем какую команду вызываем
            command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
            connection.Open();//открываем соединение
                              //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                              //добавляем параметр в вызываемую процедуру
                              //command.Parameters.AddWithValue("@Login", Login);
                              //Не добавляем.

            SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

            while (result.Read())//Пока есть что читать
            {
                Classes.AllUser_Model User_Info = new Classes.AllUser_Model(
                    int.Parse(result["id"].ToString()),
                       result["Login"].ToString(),
                       result["Password"].ToString(),
                       int.Parse(result["idOfCards"].ToString()),
                       int.Parse(result["isAdmin"].ToString())
                       );
                Classes.AllUser_Model.AllUsers.Add(User_Info);
            }

            connection.Close();
            User_List.ItemsSource = Classes.AllUser_Model.AllUsers;
            User_List.Items.Refresh();
        }

        private void AddDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Name = NameOfProduct.Text;
                DateTime dateOfDelivery;
                double Cost;
                int Amount;
                int ShippersId;
                if (DateTime.TryParse(DateOfIncoming.Text, out dateOfDelivery) && Int32.TryParse(AmountOfProduct.Text, out Amount) &&
                    Double.TryParse(CostOfProduct.Text, out Cost) && Int32.TryParse(idOfShipper.Text, out ShippersId))
                {
                    SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                    SqlCommand command = new SqlCommand("usp_DeliveryInsert", connection);//указываем какую команду вызываем
                    command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                    connection.Open();//открываем соединение
                                      //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                      //добавляем параметр в вызываемую процедуру
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@dateOfIncoming", dateOfDelivery);
                    command.Parameters.AddWithValue("@Cost", Cost);
                    command.Parameters.AddWithValue("@amount", Amount);
                    command.Parameters.AddWithValue("@idOfShipper", ShippersId);

                    SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                    //Classes.Delivery_Model.Deliverys.Clear();
                    connection.Close();
                    showDelivery();
                }
                else MessageBox.Show("Невалидные данные.");
            }
            catch(SqlException q)
            {
                MessageBox.Show(q.Message);
            }
            }

        private void showDelivery()
        {
            SqlConnection connection = new SqlConnection(Connect_data.currentConnect);

            SqlCommand command = new SqlCommand("usp_DeliverySelectAll", connection);//указываем какую команду вызываем
            command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
            connection.Open();//открываем соединение
                              //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                              //добавляем параметр в вызываемую процедуру
                              //command.Parameters.AddWithValue("@Login", Login);
                              //Не добавляем.

            SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат
            List<Classes.Delivery_Model> Deliver = new List<Classes.Delivery_Model>();
            while (result.Read())//Пока есть что читать
            {
                Classes.Delivery_Model Delivery_Info = new Classes.Delivery_Model(
                        int.Parse(result["id"].ToString()),
                           result["Name"].ToString(),
                           DateTime.Parse(result["dateOfIncoming"].ToString()),
                           double.Parse(result["Cost"].ToString()),
                           int.Parse(result["amount"].ToString()),
                           int.Parse(result["idOfShipper"].ToString())
                           );
                Deliver.Add(Delivery_Info);
            }

            connection.Close();
            Delivery_List.ItemsSource = Deliver;
            //Delivery_List.Items.Refresh();
        }

        private void AddToShop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Name = NameShop.Text;
                double Cost;
                string info=Info.Text;
                string Category=CategoryShop.Text;
                if (Double.TryParse(CostShop.Text, out Cost))
                {
                    SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                    SqlCommand command = new SqlCommand("usp_ShopInsert", connection);//указываем какую команду вызываем
                    command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                    connection.Open();//открываем соединение
                                      //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                      //добавляем параметр в вызываемую процедуру
                    command.Parameters.AddWithValue("@Category", Category);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Cost", Cost);
                    command.Parameters.AddWithValue("@dateOfIncoming", DateTime.Today);
                    command.Parameters.AddWithValue("@info", info);
                    
                    SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                    connection.Close();
                    showShop();
                }
                else MessageBox.Show("Невалидные данные.");
            }
            catch (SqlException q)
            {
                MessageBox.Show(q.Message);
            }
        }

        private void showShop()
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
            Shop_List.ItemsSource = Shop;

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                for(int i=0; i<99999;i++)
                {
                    SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
                    SqlCommand command = new SqlCommand("usp_OnlyShopInsert", connection);//указываем какую команду вызываем
                    command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                    connection.Open();//открываем соединение
                                      //MessageBox.Show("Соединение установлено");//хуитка-костыль для тупых, что соединение установлено
                                      //добавляем параметр в вызываемую процедуру
                    command.Parameters.AddWithValue("@Category", "cat"+i);
                    command.Parameters.AddWithValue("@Name", "Name"+i);
                    command.Parameters.AddWithValue("@Cost", i*2);
                    command.Parameters.AddWithValue("@dateOfIncoming", DateTime.Today);
                    command.Parameters.AddWithValue("@info", i);

                    SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                    connection.Close();
                }
                
            }
            catch (SqlException q)
            {
                MessageBox.Show(q.Message);
            }
        }

        private void Drop_Click(object sender, RoutedEventArgs e)
        {

        }
    }

   
    }

