using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using WPFProbafeladat.Models;

namespace WPFProbafeladat.Services
{
    class DatabaseLoginHandler : ILoginHandler
    {
        public bool RegistrationAllowed { get { return true; } }

        public static string HashPassword(string password, string salt)
        {
            using (var algorithm = SHA256.Create())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                byte[] hash = algorithm.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Ha már létezik user ezzel a névvel
                    if(db.Users.Any(u => u.Username == username))
                    {
                        return false;
                    }

                    string randomSalt = RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 32);
                    string saltedPassword = HashPassword(password, randomSalt);
                    var newUser = new User
                    {
                        Username = username,
                        Salt = randomSalt,
                        HashedPassword = saltedPassword
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Username == username);
                    if (user != null)
                    {
                        string calculatedHashString = HashPassword(password, user.Salt);

                        return user.HashedPassword == calculatedHashString;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
