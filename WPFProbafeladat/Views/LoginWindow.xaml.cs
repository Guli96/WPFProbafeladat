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

namespace WPFProbafeladat
{
    public interface ILoginHandler
    {
        bool ValidateUser(string username, string password);
    }

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        internal event EventHandler LoginSuccesful;

        private readonly ILoginHandler loginHandler;

        public LoginWindow(ILoginHandler loginHandler)
        {
            InitializeComponent();
            this.loginHandler = loginHandler;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            bool loginResult = loginHandler.ValidateUser(username, password);
            if (loginResult)
            {
                LoginSuccesful(this, null);
                Close();
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }
    }
}
