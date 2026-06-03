using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 
namespace AracKiralama_Otomasyonu
{
    public class CarRentalDbContext : DbContext
    {
        
        public CarRentalDbContext() : base("name=AracKiralama_Otomasyonu")
        {
        }

        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}