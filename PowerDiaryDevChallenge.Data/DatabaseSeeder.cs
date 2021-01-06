using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PowerDiaryDevChallenge.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerDiaryDevChallenge.Data
{
    /// <summary>
    /// Seeds the database with initial data if none is present
    /// </summary>
    public class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var db = new Context(serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {
                if (db.ChatEntries.Any())
                    return;

                db.ChatEntries.AddRange(
                    new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 17, 1, 22), "Bob"),
                    new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 17, 5, 35), "Kate"),
                    new ChatEntry(EventTypes.comment, new DateTime(2021, 1, 1, 17, 15, 8), "Bob", "Hey, Kate - high five?"),
                    new ChatEntry(EventTypes.high_five_another_user, new DateTime(2021, 1, 1, 17, 17, 3), "Kate", "Bob"),
                    new ChatEntry(EventTypes.leave_the_room, new DateTime(2021, 1, 1, 17, 18, 0), "Bob"),
                    new ChatEntry(EventTypes.comment, new DateTime(2021, 1, 1, 17, 20, 0), "Kate", "Oh, typical"),
                    new ChatEntry(EventTypes.leave_the_room, new DateTime(2021, 1, 1, 21, 0, 0), "Kate"),
                    new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 21, 0, 10), "Kate")
                    );

                db.SaveChanges();
            }
        }
    }
}
