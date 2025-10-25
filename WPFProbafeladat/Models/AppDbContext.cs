using Microsoft.EntityFrameworkCore;

namespace WPFProbafeladat.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProbaDb;Trusted_Connection=True;");
        }
    }
}
