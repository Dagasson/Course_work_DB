using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
   public class Shop_Model
    {
        public int id { get; set; }
        public string Category { get; set; }
        public string Name {get;set;}
        public double Cost {get;set;}
        public string onStorage {get;set;}
        public DateTime dateOfIncoming {get;set;}
        public int hmOnStorage {get;set;}
        public string info {get;set;}

        public Shop_Model() { }
        
        public Shop_Model(int id, string Category, string Name, double Cost, string onStorage, DateTime dateOfIncoming, int hmOnStorage, string info)
        {
            this.id = id;
            this.Category = Category;
            this.Name = Name;
            this.Cost = Cost;
            this.onStorage = onStorage;
            this.dateOfIncoming = dateOfIncoming;
            this.hmOnStorage = hmOnStorage;
            this.info = info;
        }
}
}
