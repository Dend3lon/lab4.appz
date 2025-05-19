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
            DataBasePath = "C:\\Users\\User\\source\\repos\\lab6.appz\\DomainData\\anticafe.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DataBasePath}");
        }
 
    }
}
