using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using AppDataAccess;
using AppDataAccess.Models;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BuyersDataAccess buyersDataAccess = new BuyersDataAccess();
        SellersDataAccess sellersDataAccess = new SellersDataAccess();
        ProductsDataAccess productsDataAccess = new ProductsDataAccess();

        ObservableCollection<Buyers> buyer = new ObservableCollection<Buyers>();
        ObservableCollection<Sellers> seller = new ObservableCollection<Sellers>();
        ObservableCollection<Products> product = new ObservableCollection<Products>();

        public Buyers selectBuyer { get; set; } = new Buyers();
        public Sellers selectSeller { get; set; } = new Sellers();
        public Products selectProduct {  get; set; } = new Products();


        
        public MainWindow()
        {
            InitializeComponent();
            fillData();

            SellersDataGrid.ItemsSource = seller;
            BuyersDataGrid.ItemsSource = buyer;
            ProductDataGrid.ItemsSource = product;
        }

        private void fillData()
        {
            buyer = buyersDataAccess.buyer;
            seller = sellersDataAccess.seller;
            product = productsDataAccess.product;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            homePanel.Visibility = Visibility.Visible;
            sellerPael.Visibility = Visibility.Collapsed;
            buyerPael.Visibility = Visibility.Collapsed;
            productPael.Visibility = Visibility.Collapsed;
        }


        // Codes related to the seller
        private void sellers_Click(object sender, RoutedEventArgs e)
        {
            homePanel.Visibility = Visibility.Collapsed;
            sellerPael.Visibility = Visibility.Visible;
            buyerPael.Visibility = Visibility.Collapsed;
            productPael.Visibility = Visibility.Collapsed;

        }

        private void SellersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SellersDataGrid.SelectedIndex >= 0)
            {
                selectSeller = SellersDataGrid.SelectedItem as Sellers;
                SellerInfo.Content = selectSeller.getBasicInfo();
            }
        }

        private void btnAddSeller_Click(object sender, RoutedEventArgs e)
        {

            AddEditSeller addseller = new AddEditSeller(sellersDataAccess);
            addseller.lblTitr.Content = "اضافه کردن فروشنده جدید";
            addseller.ShowDialog();
        }

        private void btnRemoveSeller_Click(object sender, RoutedEventArgs e)
        {
            if (SellersDataGrid.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("مطمئنی؟", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    sellersDataAccess.removeSellers(selectSeller.id);
                    MessageBox.Show("فروشنده با موفقیت حذف گردید", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);

                    SellerInfo.Content = "---";
                }
            }
           
        }

        private void btnEditeSeller_Click(object sender, RoutedEventArgs e)
        {
            if (SellersDataGrid.SelectedIndex >= 0)
            {
                AddEditSeller editseller = new AddEditSeller(sellersDataAccess, selectSeller);
                editseller.lblTitr.Content = "ویرایش اطلاعات فروشنده";

                editseller.ShowDialog();

                SellerInfo.Content = "---";
            }
        }

        // Codes related to the buyer
        private void buyers_Click(object sender, RoutedEventArgs e)
        {
            homePanel.Visibility = Visibility.Collapsed;
            sellerPael.Visibility = Visibility.Collapsed;
            buyerPael.Visibility = Visibility.Visible;
            productPael.Visibility = Visibility.Collapsed;

        }
        private void buyerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BuyersDataGrid.SelectedIndex >= 0)
            {
                selectBuyer = BuyersDataGrid.SelectedItem as Buyers;
                BuyerInfo.Content = selectBuyer.getBasicInfo();
            }

        }

        private void btnEditeBuyer_Click(object sender, RoutedEventArgs e)
        {
            if (BuyersDataGrid.SelectedIndex >= 0)
            {
                AddEditBuyer editBuyer = new AddEditBuyer(buyersDataAccess, selectBuyer);
                editBuyer.lblTitr.Content = "ویرایش اطلاعات خریدار";

                editBuyer.ShowDialog();

                BuyerInfo.Content = "---";
            }
        }

        private void btnRemoveBuyer_Click(object sender, RoutedEventArgs e)
        {
            if (BuyersDataGrid.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("مطمئنی؟", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    buyersDataAccess.removeBuyer(selectBuyer.id);
                    MessageBox.Show("خریدار با موفقیت حذف گردید", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);

                    BuyerInfo.Content = "---";
                }
            }
        }

        private void btnAddBuyer_Click(object sender, RoutedEventArgs e)
        {
            AddEditBuyer newBuyer = new AddEditBuyer(buyersDataAccess);
            newBuyer.lblTitr.Content = "اضافه کردن فروشنده جدید";

            newBuyer.ShowDialog();
        }

        // Codes related to the product
        private void products_Click(object sender, RoutedEventArgs e)
        {
            homePanel.Visibility = Visibility.Collapsed;
            sellerPael.Visibility = Visibility.Collapsed;
            buyerPael.Visibility = Visibility.Collapsed;
            productPael.Visibility = Visibility.Visible;

        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ProductDataGrid.SelectedIndex >= 0)
            {
                selectProduct = ProductDataGrid.SelectedItem as Products;
                ProductInfo.Content = selectProduct.getBasicInfo();
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddEditProduct newProduct = new AddEditProduct(productsDataAccess);

            newProduct.lblTitr.Content = "اضافه کردن کالای جدید";
            newProduct.ShowDialog();
        }

        private void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedIndex >= 0)
            {
                MessageBoxResult result = MessageBox.Show("مطمئنی؟", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    productsDataAccess.removeProduct(selectProduct.id);
                    MessageBox.Show("کالا با موفقیت حذف گردید", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);

                    BuyerInfo.Content = "---";
                }
            }
        }

        private void btnEditeProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedIndex >= 0)
            {
                AddEditProduct editProduct = new AddEditProduct(productsDataAccess, selectProduct);
                editProduct.lblTitr.Content = "ویرایش اطلاعات کالا";

                editProduct.ShowDialog();

                BuyerInfo.Content = "---";
            }
        }
    }
}
