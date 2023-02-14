using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DBCarContext : DbContext
    {

        public DBCarContext(DbContextOptions<DBCarContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RideDetails> RideDetails { get; set; }
        public DbSet<BookRide> BookedRides { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookRide>()
                .HasKey(r => new { r.rideId, r.userId });
            modelBuilder.Entity<User>()
                .Property(u => u.userId).ValueGeneratedOnAdd();
            modelBuilder.Entity<RideDetails>()
                .Property(r => r.id).ValueGeneratedOnAdd();
        }
    }   
}

