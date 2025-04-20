using DomainData.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainData
{
    public class AntiCafeContext : DbContext
    {
        public string DataBasePath { get; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public AntiCafeContext()
        {
            var path = "C:\\Users\\User\\source\\repos\\lab4.appz\\DomainData\\anticafe.db"; 
            DataBasePath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DataBasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Activities)
                .WithMany(a => a.Bookings);
        }
    }
}
