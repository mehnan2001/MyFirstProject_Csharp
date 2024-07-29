using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Models
{
    public class Products : IProducts
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int inventory { get; set; }
        public decimal price { get; set; }

        public string getBasicInfo()
        {
            string finalStr = name + "\n" + description + "\n" + inventory + "\n" + price + " $";
            return finalStr;
        }
    }
}
