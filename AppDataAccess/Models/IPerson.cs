using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Models
{
    public interface IPerson
    {
        int id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        UInt64 personalCode { get; set; }
        string address { get; set; }
        UInt64 mobileNumber { get; set; }

        string getBasicInfo();

    }
}
