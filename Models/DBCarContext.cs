using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DBCarContext : DbContext
    {

        public DBCarContext(DbContextOptions<DBCarContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RideDetails> RideDetails { get; set; }
        public DbSet<BookingDetails> BookedRides { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetails>()
                .Property(a => a.BookingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.userId).ValueGeneratedOnAdd();
            modelBuilder.Entity<RideDetails>()
                .Property(r => r.id).ValueGeneratedOnAdd();
        }
    }   
}

