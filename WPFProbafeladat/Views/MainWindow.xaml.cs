using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WPFProbafeladat.Models;
using WPFProbafeladat.Views;
namespace WPFProbafeladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var db = new AppDbContext()) 
            {
                var products = db.Products.ToList();
                ProductsDataGrid.ItemsSource = products;
            }
        }

        internal void StartupMainWindow(object sender, EventArgs e)
        {
            Application.Current.MainWindow = this;
            Show();
        }

        private void NewProduct(object sender, RoutedEventArgs e)
        {
            var productWindow = new ProductWindow();
            productWindow.ShowDialog();

            if (productWindow.IsSaved)
            {
                LoadProducts();
            }
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button?.DataContext as Product;

            if (product != null)
            {
                var productWindow = new ProductWindow(product);
                productWindow.ShowDialog();

                if (productWindow.IsSaved)
                {
                    LoadProducts();
                }
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button?.DataContext as Product;

            if (product != null)
            {
                var confirm = MessageBox.Show($"Biztosan törli ezt a terméket?\n\n{product.Code}\n{product.Name}",
                    "Törlés megerősítése",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var db = new AppDbContext())
                        {
                            db.Products.Attach(product);
                            db.Products.Remove(product);
                            db.SaveChanges();
                        }

                        LoadProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a törlés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}