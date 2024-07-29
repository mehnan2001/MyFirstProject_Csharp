using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Models
{
    public class Sellers : IPerson
    {
        public int id {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public UInt64 personalCode { get; set; }
        public string address { get; set; }
        public UInt64 mobileNumber { get; set; }
        public decimal sallary {  get; set; }

        public string getBasicInfo()
        {
            string finalStr = "full name: " + firstName + " " + lastName + 
                "\npersonal code: " + personalCode + 
                "\naddress: " + address + 
                "\nmobile: " + mobileNumber +
                "\nsallary: " + sallary; 

            return finalStr;
        }
    }
}
