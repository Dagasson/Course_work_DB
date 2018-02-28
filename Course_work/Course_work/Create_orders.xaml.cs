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
    /// Логика взаимодействия для Create_orders.xaml
    /// </summary>
    
    public partial class Create_orders : Window
    {
        public Create_orders()
        {
            InitializeComponent();
        }

        private void setNewOrders()//создаем заказ
        {
            try
            {
                SqlConnection connection = new SqlConnection(Connect_data.adminConnect);

                SqlCommand command = new SqlCommand("usp_OrdersInsert", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру
                connection.Open();//открываем соединение
                                  //добавляем параметр в вызываемую процедуру
                command.Parameters.AddWithValue("@idOfUser", User_Model.id);
                command.Parameters.AddWithValue("@Summ", 0);
                command.Parameters.AddWithValue("@dateOfAssembly", DateTime.Today);

                SqlDataReader result = command.ExecuteReader();//выполняем процедуру, попутно записывая результат

                // List<Classes.productOnOrders_Model> productOnOrders = new List<Classes.productOnOrders_Model>();

                while (result.Read())//Пока есть что читать
                {
                    Classes.Order_Model Order_Info = new Classes.Order_Model(
                        int.Parse(result["id"].ToString()),
                        int.Parse(result["idOfUser"].ToString()),
                        double.Parse(result["Summ"].ToString()),
                        DateTime.Parse(result["dateOfAssembly"].ToString())
                        );
                    Classes.Order_Model.currentId = int.Parse(result["id"].ToString());
                    //productOnOrders.Add(Order_Info);
                }

                connection.Close();
            }
            catch (SqlException q)
            {
                MessageBox.Show(q.Message);
            }
            }

        private void Place_order_Click(object sender, RoutedEventArgs e)//Оформить заказ.
        {
            try
            {
                setNewOrders();
                insertProduct();
                setSumm();
                updateSum();
                updateDate();
                Classes.productOnOrders_Model.productOnOrders.Clear();
                Close();
            }
            catch (SqlException q)
            {
                MessageBox.Show(q.Message);
            }
            }

        private void Add_on_order_Click(object sender, RoutedEventArgs e)//добавить в корзину
        {
            int idOfProd, hm;

            if ((Int32.TryParse(idOfProduct.Text, out idOfProd)) && (Int32.TryParse(how_much.Text, out hm)))
            {
                Classes.productOnOrders_Model Order_Info = new Classes.productOnOrders_Model(Classes.productOnOrders_Model.currId, idOfProd, hm);
                Classes.productOnOrders_Model.productOnOrders.Add(Order_Info);
                Classes.productOnOrders_Model.currId++;
            }
            else MessageBox.Show("Некорректные значения");

            onBasket.ItemsSource = Classes.productOnOrders_Model.productOnOrders;
            onBasket.Items.Refresh();
        }

        private void button_Click(object sender, RoutedEventArgs e)//удаление из корзины
        {
            try
            {
                int del_id;
                if (Int32.TryParse(deletedId.Text, out del_id)) Classes.productOnOrders_Model.productOnOrders.RemoveAt(del_id);
                else MessageBox.Show("Некорректный индекс.");
                onBasket.Items.Refresh();
            }
            catch(Exception q)
            {
                MessageBox.Show(q.Message);
            }
            }

        private void insertProduct()//Вставляем в бд информацию по товарам в заказе
        {
            SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
            
            foreach (Classes.productOnOrders_Model p in Classes.productOnOrders_Model.productOnOrders)
            {
                SqlCommand command = new SqlCommand("usp_productOnOrderInsert", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру

                connection.Open();//открываем соединение
                command.Parameters.AddWithValue("@idOfOrder", Classes.Order_Model.currentId);
                command.Parameters.AddWithValue("@idOfProduct", p.idOfProduct);
                command.Parameters.AddWithValue("@amount", p.amount);

                SqlDataReader result = command.ExecuteReader();
                connection.Close();
            }
            
        }

        private void setSumm()//Вставляем в бд информацию по товарам в заказе
        {
            SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
            SqlCommand command = new SqlCommand("usp_getSummOfOrder", connection);//указываем какую команду вызываем
                command.CommandType = System.Data.CommandType.StoredProcedure;// указываем, что команда представляет хранимую процедуру

                connection.Open();//открываем соединение
                command.Parameters.AddWithValue("@idOfOrder", Classes.Order_Model.currentId);
                
                SqlDataReader result = command.ExecuteReader();
                if (result.Read())
                {
                    Classes.Order_Model.currentSum = double.Parse(result["summ"].ToString());
                    MessageBox.Show(Classes.Order_Model.currentSum.ToString());
                }
                
                connection.Close();
        }

        private void updateSum()
        {
            SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
            SqlCommand command = new SqlCommand("usp_OrdersSumUpdate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@id", Classes.Order_Model.currentId);
            command.Parameters.AddWithValue("@Summ", Classes.Order_Model.currentSum);

            SqlDataReader result = command.ExecuteReader();
            connection.Close();
        }
        private void updateDate()
        {
            SqlConnection connection = new SqlConnection(Connect_data.adminConnect);
            SqlCommand command = new SqlCommand("usp_OrdersDateUpdate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@id", Classes.Order_Model.currentId);

            SqlDataReader result = command.ExecuteReader();
            connection.Close();
        }
    }
}
