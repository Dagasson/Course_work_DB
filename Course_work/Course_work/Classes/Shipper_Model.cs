using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class Shipper_Model
    {
        public int id { get; set; }
        public string name { get; set; }//Это название фирмы-поставщика, в Delivery это будет название товара
        public int numberOfCards { get; set; }

        public static List<Classes.Shipper_Model> Shippers = new List<Classes.Shipper_Model>();


        public Shipper_Model(int id, string name, int numberOfCards)
        {
            this.id = id;
            this.name = name;
            this.numberOfCards = numberOfCards;
        }

        public Shipper_Model() { }
    }
}
