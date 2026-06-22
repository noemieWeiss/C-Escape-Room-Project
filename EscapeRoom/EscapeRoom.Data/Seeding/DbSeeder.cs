using EscapeRoom.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.DifficultyLevels.AnyAsync())
                return;

            var difficulties = new Dictionary<string, DifficultyLevel>
            {
                ["Easy"] = new() { Name = "Easy" },
                ["Medium"] = new() { Name = "Medium" },
                ["Hard"] = new() { Name = "Hard" },
            };
            context.DifficultyLevels.AddRange(difficulties.Values);
            await context.SaveChangesAsync();

            var rooms = new List<EscapeRoomEntity>
            {
                new() { Name = "The Haunted Library", DifficultyLevelId = difficulties["Easy"].Id, MaxParticipants = 6 },
                new() { Name = "Pirate's Treasure Cove", DifficultyLevelId = difficulties["Easy"].Id, MaxParticipants = 8 },
                new() { Name = "Cyber Lab Breakout", DifficultyLevelId = difficulties["Medium"].Id, MaxParticipants = 5 },
                new() { Name = "Detective's Office", DifficultyLevelId = difficulties["Medium"].Id, MaxParticipants = 6 },
                new() { Name = "Pharaoh's Tomb", DifficultyLevelId = difficulties["Hard"].Id, MaxParticipants = 4 },
                new() { Name = "Zombie Apocalypse Bunker", DifficultyLevelId = difficulties["Hard"].Id, MaxParticipants = 5 },
            };
            context.EscapeRooms.AddRange(rooms);
            await context.SaveChangesAsync();

            var players = new List<Player>
            {
                new() { FullName = "Noa Cohen", Email = "noa.cohen@example.com", Phone = "050-1111111" },
                new() { FullName = "David Levi", Email = "david.levi@example.com", Phone = "050-2222222" },
                new() { FullName = "Maya Shapira", Email = "maya.shapira@example.com", Phone = "050-3333333" },
                new() { FullName = "Yosef Mizrahi", Email = "yosef.mizrahi@example.com", Phone = null },
                new() { FullName = "Shira Ben-Ami", Email = "shira.benami@example.com", Phone = "050-5555555" },
            };
            context.Players.AddRange(players);
            await context.SaveChangesAsync();

            context.Hints.AddRange(
                new Hint { EscapeRoomId = rooms[0].Id, Text = "Check the books arranged by color on the top shelf." },
                new Hint { EscapeRoomId = rooms[0].Id, Text = "The librarian's desk drawer needs a four-digit year." },
                new Hint { EscapeRoomId = rooms[2].Id, Text = "The server password is hidden in the network diagram." },
                new Hint { EscapeRoomId = rooms[3].Id, Text = "Look at the fingerprints under the desk lamp." },
                new Hint { EscapeRoomId = rooms[4].Id, Text = "The hieroglyphs on the west wall form a sequence." },
                new Hint { EscapeRoomId = rooms[5].Id, Text = "The emergency radio frequency is written on the supply crate." }
            );
            await context.SaveChangesAsync();

            context.Bookings.AddRange(
                new Booking { PlayerId = players[0].Id, EscapeRoomId = rooms[0].Id, BookingDateTime = new DateTime(2026, 7, 10, 18, 0, 0), NumberOfParticipants = 4, Status = "Confirmed" },
                new Booking { PlayerId = players[1].Id, EscapeRoomId = rooms[2].Id, BookingDateTime = new DateTime(2026, 7, 12, 20, 0, 0), NumberOfParticipants = 3, Status = "Pending" },
                new Booking { PlayerId = players[2].Id, EscapeRoomId = rooms[4].Id, BookingDateTime = new DateTime(2026, 7, 15, 19, 30, 0), NumberOfParticipants = 4, Status = "Confirmed" },
                new Booking { PlayerId = players[3].Id, EscapeRoomId = rooms[1].Id, BookingDateTime = new DateTime(2026, 7, 18, 17, 0, 0), NumberOfParticipants = 6, Status = "Pending" },
                new Booking { PlayerId = players[4].Id, EscapeRoomId = rooms[3].Id, BookingDateTime = new DateTime(2026, 7, 20, 21, 0, 0), NumberOfParticipants = 5, Status = "Confirmed" },
                new Booking { PlayerId = players[0].Id, EscapeRoomId = rooms[5].Id, BookingDateTime = new DateTime(2026, 7, 22, 16, 0, 0), NumberOfParticipants = 4, Status = "Cancelled" }
            );
            await context.SaveChangesAsync();
        }
    }
}
