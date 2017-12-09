using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class Order_Model
    {
        public int id { get; set; }
        public int idOfUser { get; set; }
        public double Summ { get; set; }
        public DateTime DateOfAssembly { get; set; }

        public Order_Model(int id, int idOfUser, double Summ, DateTime DateOfAssembly)
        {
            this.id = id;
            this.idOfUser = idOfUser;
            this.Summ = Summ;
            this.DateOfAssembly = DateOfAssembly;
        }
        public Order_Model() { }

    }
}
