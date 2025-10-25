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

namespace WPFProbafeladat.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly ILoginHandler _loginHandler;
        public RegistrationWindow(ILoginHandler loginHandler)
        {
            InitializeComponent();
            _loginHandler = loginHandler;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("A mezők kitöltése kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool result = _loginHandler.RegisterUser(UsernameTextBox.Text, PasswordBox.Password);
            if (result)
            {
                MessageBox.Show("Sikeres regisztráció!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sikertelen regisztráció! Felhasználónév már létezik, vagy adatbázis hiba.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
