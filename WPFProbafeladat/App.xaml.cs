using System.Configuration;
using System.Data;
using System.Windows;
using WPFProbafeladat.Services;

namespace WPFProbafeladat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ILoginHandler handler = new LocalLoginHandler();

            LoginWindow loginWindow = new LoginWindow(handler);
            MainWindow mainWindow = new MainWindow();

            Application.Current.MainWindow = loginWindow;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            loginWindow.LoginSuccesful += mainWindow.StartupMainWindow;

            loginWindow.Show();
        }
    }
}
