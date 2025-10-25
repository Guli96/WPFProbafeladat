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
using WPFProbafeladat.Models;

namespace WPFProbafeladat.Views
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private Product? _product;
        public bool IsSaved { get; private set; } = false;

        public ProductWindow()
        {
            InitializeComponent();
            Title = "Új termék";
            _product = null;
        }

        public ProductWindow(Product product)
        {
            InitializeComponent();
            Title = "Termék szerkesztése";
            _product = product;

            CodeTextBox.Text = product.Code;
            NameTextBox.Text = product.Name;
            DescriptionTextBox.Text = product.Description;
            PriceTextBox.Text = product.Price.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CodeTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                !decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Kérem ellenőrizze a kitöltött mezők helyességét!", "Hiányos adatok",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    if (_product == null)
                    {
                        var newProduct = new Product
                        {
                            Code = CodeTextBox.Text,
                            Name = NameTextBox.Text,
                            Description = DescriptionTextBox.Text,
                            Price = price
                        };
                        db.Products.Add(newProduct);
                    }
                    else
                    {
                        var existingProduct = db.Products.Find(_product.Id);
                        if (existingProduct != null)
                        {
                            existingProduct.Code = CodeTextBox.Text;
                            existingProduct.Name = NameTextBox.Text;
                            existingProduct.Description = DescriptionTextBox.Text;
                            existingProduct.Price = price;
                        }
                    }

                    db.SaveChanges();
                    IsSaved = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}", "Hiba", MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
