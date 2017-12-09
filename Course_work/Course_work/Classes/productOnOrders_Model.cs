using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class productOnOrders_Model
    {
        public int id;
        public int idOfOrder;
        public int idOfProduct;
        public int amount;

        public productOnOrders_Model(int id, int idOfOrder, int idOfProduct ,int amount)
        {
            this.id = id;
            this.idOfOrder = idOfOrder;
            this.idOfProduct = idOfProduct;
            this.amount = amount;
        }

        public productOnOrders_Model() { }
    }
}
