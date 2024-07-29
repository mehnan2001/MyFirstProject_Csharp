using AppDataAccess.Models;
using AppDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using AppDataAccess;
using AppDataAccess.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddEditBuyer.xaml
    /// </summary>
    public partial class AddEditBuyer : Window
    {
        private BuyersDataAccess buyerDataAccess;
        private Buyers buyer;
        private bool isEdit = false;
        public AddEditBuyer(BuyersDataAccess byrDataAccess)
        {
            InitializeComponent();
            buyerDataAccess = byrDataAccess;
        }

        public AddEditBuyer(BuyersDataAccess byrDataAccess, Buyers BuyerInfo)
        {
            InitializeComponent();
            buyerDataAccess = byrDataAccess;
            buyer = BuyerInfo;
            isEdit = true;

            textBoxInitialization();

        }

        private void textBoxInitialization()
        {
            tbFirstName.Text = buyer.firstName;
            tbLastName.Text = buyer.lastName;
            tbAddress.Text = buyer.address;
            tbMobileNumber.Text = Convert.ToString(buyer.mobileNumber);
            tbPersonalCode.Text = Convert.ToString(buyer.personalCode);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (isEdit)
                {


                    Buyers updateBuyer = new Buyers()
                    {
                        id = buyer.id,
                        firstName = tbFirstName.Text.Trim(),
                        lastName = tbLastName.Text.Trim(),
                        address = tbAddress.Text.Trim(),
                        mobileNumber = Convert.ToUInt64(tbMobileNumber.Text.Trim()),
                        personalCode = Convert.ToUInt64(tbPersonalCode.Text.Trim()),
                    };


                    // Check for changes in information
                    bool isChange = checkChangeValid();

                    if (isChange)
                    {
                        // Check for validations input
                        bool isValid = checkValidation();
                        if (isValid)
                        {
                            MessageBoxResult res = MessageBox.Show("از اعمال تغییرات اطمینان دارید؟", "اخطار",
                                MessageBoxButton.YesNo, MessageBoxImage.Warning);


                            if (res == MessageBoxResult.Yes)
                            {
                                buyerDataAccess.editeBuyer(updateBuyer);
                                MessageBox.Show("ویرایش با موفقیت انجام شد", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                                textBoxInitialization();
                        }
                    }
                    else
                        this.Close();
                }

                else
                {
                    // Check for validations input
                    bool isValid = checkValidation();

                    if (isValid)
                    {

                        Buyers newBuyer = new Buyers()
                        {
                            id = buyerDataAccess.getNextId(),
                            firstName = tbFirstName.Text.Trim(),
                            lastName = tbLastName.Text.Trim(),
                            address = tbAddress.Text.Trim(),
                            mobileNumber = Convert.ToUInt64(tbMobileNumber.Text.Trim()),
                            personalCode = Convert.ToUInt64(tbPersonalCode.Text.Trim()),
                        };

                        buyerDataAccess.addBuyer(newBuyer);
                        MessageBox.Show("فروشنده جدید با موفقیت اضافه شد", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();

                    }
                }





            }
            catch (Exception)
            {
                MessageBox.Show("خطایی پیش آمده! لطفا با پشتیبان تماس بگیرید", "خطا",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool checkChangeValid()
        {
            bool isChange = false;

            if (tbFirstName.Text != buyer.firstName ||
                tbLastName.Text != buyer.lastName ||
                tbAddress.Text != buyer.address ||
                tbMobileNumber.Text != Convert.ToString(buyer.mobileNumber) ||
                tbPersonalCode.Text != Convert.ToString(buyer.personalCode))
            {
                isChange = true;
            }

            return isChange;
        }

        private bool checkValidation()
        {
            bool isValid = true;

            string firstName = tbFirstName.Text.Trim();
            string lastName = tbLastName.Text.Trim();
            string address = tbAddress.Text.Trim();
            string mobileNumber = tbMobileNumber.Text.Trim();
            string personalCode = tbPersonalCode.Text.Trim();

            if (string.IsNullOrEmpty(firstName))
            {
                isValid = false;
                MessageBox.Show("لطفا نام را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                isValid = false;
                MessageBox.Show("لطفا نام خانوادگی را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!UInt64.TryParse(personalCode, out UInt64 p))
            {
                isValid = false;
                MessageBox.Show("لطفا کدملی را به صورت صحیح وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(address))
            {
                isValid = false;
                MessageBox.Show("لطفا آدرس را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!UInt64.TryParse(mobileNumber, out UInt64 m))
            {
                isValid = false;
                MessageBox.Show("لطفا شماره تماس را به صورت صحیح وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isValid;

        }

    }


}

