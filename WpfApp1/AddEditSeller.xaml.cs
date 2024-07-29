using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using AppDataAccess;
using AppDataAccess.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddEditSeller.xaml
    /// </summary>

    public partial class AddEditSeller : Window
    {

        private SellersDataAccess sellerDataAccess;
        private Sellers seller;
        private bool isEdit = false;
        public AddEditSeller(SellersDataAccess slrDataAccess)
        {
            InitializeComponent();
            sellerDataAccess = slrDataAccess;
        }

        public AddEditSeller(SellersDataAccess slrDataAccess, Sellers sellerInfo)
        {
            InitializeComponent();
            sellerDataAccess = slrDataAccess;
            seller = sellerInfo;
            isEdit = true;

            textBoxInitialization();

        }

        private void textBoxInitialization()
        {
            tbFirstName.Text = seller.firstName;
            tbLastName.Text = seller.lastName;
            tbAddress.Text = seller.address;
            tbMobileNumber.Text = Convert.ToString(seller.mobileNumber);
            tbPersonalCode.Text = Convert.ToString(seller.personalCode);
            tbsallary.Text = Convert.ToString(seller.sallary);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (isEdit)
                {


                    Sellers updateSeller = new Sellers()
                    {
                        id = seller.id,
                        firstName = tbFirstName.Text.Trim(),
                        lastName = tbLastName.Text.Trim(),
                        address = tbAddress.Text.Trim(),
                        mobileNumber = Convert.ToUInt64(tbMobileNumber.Text.Trim()),
                        personalCode = Convert.ToUInt64(tbPersonalCode.Text.Trim()),
                        sallary = Convert.ToDecimal(tbsallary.Text.Trim())
                    };


                    // Check for changes in information
                    bool isChange = checkChangeValue();
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
                                sellerDataAccess.editeSellers(updateSeller);
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

                        Sellers newSeller = new Sellers()
                        {
                            id = sellerDataAccess.getNextId(),
                            firstName = tbFirstName.Text.Trim(),
                            lastName = tbLastName.Text.Trim(),
                            address = tbAddress.Text.Trim(),
                            mobileNumber = Convert.ToUInt64(tbMobileNumber.Text.Trim()),
                            personalCode = Convert.ToUInt64(tbPersonalCode.Text.Trim()),
                            sallary = Convert.ToDecimal(tbsallary.Text.Trim())

                        };

                        sellerDataAccess.addSellers(newSeller);
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

        private bool checkChangeValue()
        {
            bool isChange = false;

            if (tbFirstName.Text != seller.firstName ||
                tbLastName.Text != seller.lastName ||
                tbAddress.Text != seller.address ||
                tbMobileNumber.Text != Convert.ToString(seller.mobileNumber) ||
                tbPersonalCode.Text != Convert.ToString(seller.personalCode) ||
                tbsallary.Text != Convert.ToString(seller.sallary))
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
            string sallary = tbsallary.Text.Trim();

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

            else if (!UInt64.TryParse(sallary, out UInt64 s))
            {
                isValid = false;
                MessageBox.Show("لطفا میزان حقوق را به صورت صحیح وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isValid;

        }

    }

    
}
