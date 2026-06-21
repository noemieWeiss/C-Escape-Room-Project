using EscapeRoom.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Room> Rooms { get; set; } // שינינו פה ל-Rooms!
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hint> Hints { get; set; }
    }
}