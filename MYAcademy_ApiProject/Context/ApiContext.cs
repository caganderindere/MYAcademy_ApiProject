using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-Q7KL8HK\\SQLEXPRESS;Database=ApiNewDemoDb;Integrated Security=True;TrustServerCertificate=True"
            );
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}