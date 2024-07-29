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
    public class SellersDataAccess
    {
        private string path = @"./DBseller.csv";

        private void readSellers()
        {
            using (var reader = new StreamReader(path))
            {
                seller.Clear();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    Sellers slr = new Sellers()
                    {
                        id = Convert.ToInt32(values[0].Trim()),
                        firstName = values[1].Trim(),
                        lastName = values[2].Trim(),
                        personalCode = Convert.ToUInt64(values[3].Trim()),
                        address = values[4].Trim(),
                        mobileNumber = Convert.ToUInt64(values[5].Trim()),
                        sallary = Convert.ToDecimal(values[6].Trim()),
                    };

                    seller.Add(slr);
                }
            }
        }

        private void saveSellers()
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (Sellers slr in seller)
                {
                    string id = slr.id.ToString();
                    string firstName = slr.firstName;
                    string lastName = slr.lastName;
                    string personalCode = slr.personalCode.ToString();
                    string address = slr.address;
                    string mobileNumber = slr.mobileNumber.ToString();
                    string sallary = slr.sallary.ToString();

                    string line = string.Format("{0};{1};{2};{3};{4};{5};{6}",
                        id, firstName, lastName, personalCode, address, mobileNumber, sallary);

                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }


        public ObservableCollection<Sellers> seller { get; set; } = new ObservableCollection<Sellers>();
        public SellersDataAccess()
        {
            readSellers();
        }



        public void addSellers(Sellers newSellers)
        {
            seller.Add(newSellers);
            saveSellers();
        }

        public void removeSellers(int id)
        {
            Sellers temp = seller.First(x => x.id == id);
            seller.Remove(temp);
            saveSellers();
        }

        public void editeSellers(Sellers updateSeller)
        {
            Sellers temp = seller.First(x => x.id == updateSeller.id);
            int index = seller.IndexOf(temp);
            seller[index] = updateSeller;
            saveSellers();

        }

        public int getNextId()
        {
            int index = seller.Any() ? seller.Max(x => x.id) + 1 : 1;
            return index;
        }
    }
}
