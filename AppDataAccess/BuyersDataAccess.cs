using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AppDataAccess.Models;


namespace AppDataAccess
{
    public class BuyersDataAccess
    {

        private string path = @"./DBbuyer.csv";

        private void readBuyers()
        {
            using (var reader = new StreamReader(path))
            {
                buyer.Clear();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    Buyers byr = new Buyers()
                    {
                        id = Convert.ToInt32(values[0].Trim()),
                        firstName = values[1].Trim(),
                        lastName = values[2].Trim(),
                        personalCode = Convert.ToUInt64(values[3].Trim()),
                        address = values[4].Trim(),
                        mobileNumber = Convert.ToUInt64(values[5].Trim()),
                    };

                    buyer.Add(byr);
                }
            }
        }

        private void saveBuyers()
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (Buyers byr in buyer)
                {
                    string id = byr.id.ToString();
                    string firstName = byr.firstName;
                    string lastName = byr.lastName;
                    string personalCode = byr.personalCode.ToString();
                    string address = byr.address;
                    string mobileNumber = byr.mobileNumber.ToString();

                    string line = string.Format("{0};{1};{2};{3};{4};{5};{6}",
                        id, firstName, lastName, personalCode, address, mobileNumber);

                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }


        public ObservableCollection<Buyers> buyer { get; set; } = new ObservableCollection<Buyers>();
        public BuyersDataAccess()
        {
            readBuyers();
        }

        public void addBuyer(Buyers newBuyer)
        {
            buyer.Add(newBuyer);
            saveBuyers();
        }

        public void removeBuyer(int id)
        {
            Buyers temp = buyer.First(x => x.id == id);
            buyer.Remove(temp);
            saveBuyers();
        }

        public void editeBuyer(Buyers updateBuyer)
        {
            Buyers temp = buyer.First(x => x.id == updateBuyer.id);
            int index = buyer.IndexOf(temp);
            buyer[index] = updateBuyer;
            saveBuyers();
        }

        public int getNextId()
        {
            int index = buyer.Any() ? buyer.Max(x => x.id) + 1 : 1;
            return index;
        }
    }
}
