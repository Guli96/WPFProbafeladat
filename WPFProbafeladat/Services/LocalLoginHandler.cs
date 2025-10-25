using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFProbafeladat.Services
{
    public class LocalLoginHandler : ILoginHandler
    {
        public bool RegistrationAllowed { get { return false; } }

        public bool RegisterUser(string username, string password)
        {
            return false;
        }

        public bool ValidateUser(string username, string password)
        {
            return(username == "admin" && password == "admin");
        }
    }
}
