using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    class Delivery_Model
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime dateOfIncoming { get; set; }
        public double Cost { get; set; }
        public int amount { get; set; }
        public int idOfShipper { get; set; }

        static public List<Delivery_Model> Deliverys = new List<Delivery_Model>();

        public Delivery_Model(int id, string Name, DateTime dateOfIncoming, double Cost, int amount, int idOfShipper)
        {
            this.id = id;
            this.Name = Name;
            this.dateOfIncoming = dateOfIncoming;
            this.Cost = Cost;
            this.amount = amount;
            this.idOfShipper = idOfShipper;
        }

        public Delivery_Model() { }
    }
}
