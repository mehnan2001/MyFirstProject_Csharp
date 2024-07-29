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
    /// Interaction logic for AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Window
    {
        private ProductsDataAccess productDataAccess;
        private Products product;
        private bool isEdit;

        public AddEditProduct(ProductsDataAccess prd)
        {
            InitializeComponent();
            productDataAccess = prd;
        }

        public AddEditProduct(ProductsDataAccess prdDataAccess, Products productInfo)
        {
            InitializeComponent();
            productDataAccess = prdDataAccess;
            product = productInfo;
            isEdit = true;

            textBoxInitialization();
        }

        private void textBoxInitialization()
        {
            tbProductName.Text = product.name;
            tbDescription.Text = product.description;
            tbInventory.Text = Convert.ToString(product.inventory);
            tbPrice.Text = Convert.ToString(product.price);
        }


        private void btnCancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (isEdit)
                {
                    Products updateProduct = new Products()
                    {
                        id = product.id,
                        name = tbProductName.Text.Trim(),
                        description = tbDescription.Text.Trim(),
                        inventory = Convert.ToInt32(tbInventory.Text.Trim()),
                        price = Convert.ToDecimal(tbPrice.Text.Trim())
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
                                productDataAccess.editeProduct(updateProduct);
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
                        Products newProduct = new Products
                        {
                            id = productDataAccess.getNextId(),
                            name = tbProductName.Text.Trim(),
                            description = tbDescription.Text.Trim(),
                            inventory = Convert.ToInt32(tbInventory.Text.Trim()),
                            price = Convert.ToDecimal(tbPrice.Text.Trim())
                        };

                        productDataAccess.addProduct(newProduct);
                        MessageBox.Show("کالای جدید با موفقیت اضافه شد", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("خطایی پیش آمده! لطفا با پشتیبان تماس بگیرید", "خطا",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private bool checkChangeValue()
        {
            bool isChange = false;

            if (tbProductName.Text != product.name ||
                tbDescription.Text != product.description ||
                tbInventory.Text != Convert.ToString(product.inventory) ||
                tbPrice.Text != Convert.ToString(product.price))
            { 
                isChange = true; 
            }

            return isChange;
        }

        private bool checkValidation()
        {
            bool isValid = true;

            string name = tbProductName.Text.Trim();
            string description = tbDescription.Text.Trim();
            string inventory = tbInventory.Text.Trim();
            string price = tbPrice.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                isValid = false;
                MessageBox.Show("لطفا نام کالا را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (string.IsNullOrEmpty(description))
            {
                isValid = false;
                MessageBox.Show("لطفا توضیحات کالا را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (string.IsNullOrEmpty(inventory))
            {
                isValid = false;
                MessageBox.Show("لطفا میزان موجودی کالا را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (!int.TryParse(inventory, out int i))
            {
                isValid = false;
                MessageBox.Show("برای موجودی کالا فقط عدد وارد کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (string.IsNullOrEmpty(price))
            {
                isValid = false;
                MessageBox.Show("لطفا قیمت کالا را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if(!decimal.TryParse(price, out decimal d))
            {
                isValid = false;
                MessageBox.Show("برای قیمت کالا فقط عدد وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            return isValid;
        }
    }
}
