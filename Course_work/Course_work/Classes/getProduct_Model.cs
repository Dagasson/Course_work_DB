using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Classes
{
    public class getProduct_Model
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }

       public getProduct_Model(string Name, string Category, int amount, DateTime date)
        {
            this.Name = Name;
            this.Category = Category;
            this.amount = amount;
            this.date = date;
        }

    }
}
