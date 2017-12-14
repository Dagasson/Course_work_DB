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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void registry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Login = login.Text;
                string Password = password.Text;
                int NumOfCards;
                int isAdmin = 1;
                if (Int32.TryParse(numOfCard.Text, out NumOfCards))
                {
                    SqlConnection connection = new SqlConnection(Connect_data.defaultConnect);

                    SqlCommand command = new SqlCommand("usp_UsersInsert", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@idOfCards", NumOfCards);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);

                    SqlDataReader result = command.ExecuteReader();
                    connection.Close();
                    MessageBox.Show("Регистрация прошла успешно.");
                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
