using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class Order_Model
    {
        public static double currentSum { get; set; }
        public static int currentId { get; set; }
        public int id { get; set; }
        public int idOfUser { get; set; }
        public double Summ { get; set; }
        public DateTime dateOfAssembly { get; set; }

        public Order_Model(int id, int idOfUser, double Summ, DateTime dateOfAssembly)
        {
            this.id = id;
            this.idOfUser = idOfUser;
            this.Summ = Summ;
            this.dateOfAssembly = dateOfAssembly;
        }
        public Order_Model() { }

    }
}
