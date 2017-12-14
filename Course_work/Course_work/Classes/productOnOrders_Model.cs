using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class productOnOrders_Model
    {
        static public int currId=0;//для нумерации в корзине.
        public int id { get; set; }
        public int idOfOrder { get; set; }
        public int idOfProduct { get; set; }
        public int amount { get; set; }

        public static List<Classes.productOnOrders_Model> productOnOrders = new List<Classes.productOnOrders_Model>();

        public productOnOrders_Model(int id, int idOfOrder, int idOfProduct ,int amount)
        {
            this.id = id;
            this.idOfOrder = idOfOrder;
            this.idOfProduct = idOfProduct;
            this.amount = amount;
        }

        public productOnOrders_Model(int id, int idOfProduct, int amount)
        {
            this.id = id;
        //    this.idOfOrder = idOfOrder;
            this.idOfProduct = idOfProduct;
            this.amount = amount;
        }
        public productOnOrders_Model(int idOfProduct, int amount)
        {
            //   this.id = id;
         //   this.idOfOrder = idOfOrder;
            this.idOfProduct = idOfProduct;
            this.amount = amount;
        }

        public productOnOrders_Model() { }
    }
}
