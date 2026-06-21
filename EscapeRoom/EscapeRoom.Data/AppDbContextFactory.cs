using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscapeRoom.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // כאן הגדרת המשתנה שהייתה חסרה:
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // שימוש בכתובת IP מקומית מפורשת
optionsBuilder.UseSqlServer("Server=localhost;Database=EscapeRoomDB;Trusted_Connection=True;TrustServerCertificate=True;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}