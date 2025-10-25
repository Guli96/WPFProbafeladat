using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WPFProbafeladat.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
