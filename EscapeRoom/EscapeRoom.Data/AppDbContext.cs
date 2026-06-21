using Microsoft.EntityFrameworkCore;
using EscapeRoom.Core.Entities;

namespace EscapeRoom.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // הגדרת הישויות כטבלאות בבסיס הנתונים (DbSet)
        public DbSet<Room> Rooms { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hint> Hints { get; set; }

        // פונקציה לעיצוב חוקי ה-DB, מפתחות ואינדקסים (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. הגדרת אינדקס ואילוץ ייחודי על האימייל של השחקן
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Email)
                .IsUnique()
                .HasDatabaseName("idx_players_email");

            // 2. הגדרת אינדקס על תאריך ושעת ההזמנה (בשביל בדיקת זמינות מהירה ב-Core)
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.BookingDate)
                .HasDatabaseName("idx_bookings_datetime");

            // 3. הגדרת אינדקסים על המפתחות הזרים בטבלת ההזמנות לשיפור ביצועי JOIN
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.PlayerId)
                .HasDatabaseName("idx_bookings_player_id");

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.RoomId)
                .HasDatabaseName("idx_bookings_room_id");

            // 4. הגדרת אינדקס על מפתח זר של רמת הקושי בחדרים (לסינונים מהירים באתר)
            modelBuilder.Entity<Room>()
                .HasIndex(e => e.Difficulty)
                .HasDatabaseName("idx_rooms_difficulty");
        }
    }
}