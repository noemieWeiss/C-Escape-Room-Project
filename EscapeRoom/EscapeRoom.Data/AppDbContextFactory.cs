using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EscapeRoom.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found in appsettings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var apiProjectPath = ResolveApiProjectPath();

            return new ConfigurationBuilder()
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
        }

        private static string ResolveApiProjectPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var candidates = new[]
            {
                Path.Combine(currentDirectory, "..", "EscapeRoom.API"),
                Path.Combine(currentDirectory, "EscapeRoom", "EscapeRoom.API"),
                Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "EscapeRoom.API"))
            };

            foreach (var candidate in candidates)
            {
                var fullPath = Path.GetFullPath(candidate);
                if (File.Exists(Path.Combine(fullPath, "appsettings.json")))
                    return fullPath;
            }

            throw new InvalidOperationException(
                "Could not locate EscapeRoom.API/appsettings.json for design-time DbContext configuration.");
        }
    }
}
