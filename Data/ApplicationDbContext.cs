using Microsoft.EntityFrameworkCore;
using pocket_service.Models;

namespace pocket_service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Address> Address {get; set;}
        public DbSet<CarService> CarServices { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
