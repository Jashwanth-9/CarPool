using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DBCarContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CarPoolDb;Trusted_Connection=True;Encrypt=False");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<UserRide> UserRides { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRide>()
                .HasKey(r => new { r.rideId, r.userId });
        }
    }   
}

