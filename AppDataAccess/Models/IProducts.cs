using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Models
{
    public interface IProducts
    {
        int id { get; set; }
        string name { get; set; }
        string description { get; set; }
        int inventory { get; set; }
        decimal price { get; set; }

        string getBasicInfo();


    }
}
