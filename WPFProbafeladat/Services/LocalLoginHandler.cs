using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProbafeladat.Services
{
    public class LocalLoginHandler : ILoginHandler
    {
        public bool ValidateUser(string username, string password)
        {
            return(username == "admin" && password == "admin");
        }
    }
}
