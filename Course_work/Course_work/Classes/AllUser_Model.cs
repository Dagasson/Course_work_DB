using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class AllUser_Model
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int idOfCards { get; set; }
        public int id { get; set; }
        public int isAdmin { get; set; }

        static public List<AllUser_Model> AllUsers = new List<AllUser_Model>();

        public AllUser_Model(int id, string Login, string Password, int idOfCards, int isAdmin)
        {
            this.id = id;
            this.Login = Login;
            this.Password = Password;
            this.idOfCards = idOfCards;
            this.isAdmin = isAdmin;
        }

        public AllUser_Model() { }
    }
}
