using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Course_work
{
    class Connect_data//Подключение здорового человека.
    {
        static public string adminConnect = "Data Source = ASHENVALE;Integrated Security = false; User ID=IS_admin; Password=IS_admin; ApplicationIntent=ReadWrite;";
        static public string userConnect = "Data Source = ASHENVALE;Integrated Security = false; User ID=IS_user; Password=IS_user;ApplicationIntent=ReadWrite;";
        static public string defaultConnect = "Data Source = ASHENVALE; Initial Catalog = Course_work; Integrated Security = true;";
    }
}
