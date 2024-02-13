using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreDAL.Data
{
    public static class SeederDB
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var context = serviceProvider.GetRequiredService<StoreContext>();

            if (!userManager.Users.Any())
            {
                await roleManager.CreateAsync(new Role { Name = "Member" });
                await roleManager.CreateAsync(new Role { Name = "Admin" });

                var user = new User
                {
                    FirstName = "Stepan",
                    LastName = "Nepan",
                    UserName = "StenNepan",
                    Email = "StenNepan@gmail.com",
                };

                await userManager.CreateAsync(user, "12345678");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                await userManager.CreateAsync(admin, "12345678");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }

            var countries = new List<Country>
            {
                new Country
                {
                    Name = "Japan",
                    Description = "Japan is a country located on an archipelago in East Asia, defined by a peculiar mixture of modernity and tradition. It is known for its unique culture," +
                    " which includes tea drinking, ikebana (flower art), garden art and traditional noh theater. Japan is also famous for its innovations and technological achievements in" +
                    " the fields of automotive, electronics and robotics. The country has impressive natural beauty spots such as the Fuji Mountains and traditional spa resorts. Japan is a " +
                    "country with a rich history, unique traditions and a modern society."
                },

                new Country
                {
                    Name = "Germany",
                    Description = "Germany impresses with the diversity of its cultural heritage and its many attractions. The country is famous for its majestic art collections in museums " +
                    "such as the State Art Gallery in Dresden and the Museum der Ischluss in Munich. The literary heritage of Goethe and Schiller, theater performances in Bayreuth, and music " +
                    "festivals such as the Bayreuth Opera Festival define the German cultural scene. Historical landmarks such as Cologne Cathedral and Neuschwanstein Castle add to the country's " +
                    "unique character."
                },

                new Country
                {
                    Name = "Italy",
                    Description = "A country with a rich history, from the Roman Empire to the Renaissance. Rome, as the capita is marked by ancient monuments. The culmination of art, architecture" +
                    " and cuisine. Recognized for its museums, opera and wine.Located on the Apennine Peninsula, with many outstanding natural places, including the Amalfi Coast and lakes. The world" +
                    " center of fashion and design.Cities such as Milan are famous for their shopping streets and prominent fashion events."
                },

                new Country
                {
                    Name = "Greece",
                    Description = "Located on the Balkan Peninsula, Greece is considered the cradle of ancient Greek civilization and democracy. It is noteworthy for its ancient monuments, such as the " +
                    "Acropolis in Athens, the Epidaurus Theater and the Temple of Olympia. The country is considered to be the cradle of ancient Greek mythology and philosophy, with prominent philosophers " +
                    "such as Socrates, Plato and Aristotle. Greek islands such as Crete, Santorini, and Mykonos are famous for their crystal water, traditional architecture, and sunny climate."
                },

                new Country
                {
                    Name = "Poland",
                    Description = "Located in the center of Europe, Poland has a rich cultural and historical past, the influence of which is e in its architecture, traditions and cuisine. Poland has " +
                    "witnessed great historical events, including the cre the Kingdom of Poland and its entry into the United Kingdom with Lithuania. Poland's castles and fortre such as Wawel in " +
                    "Krakow and Malbork in the northwest, bear witness to the greatness of bygone eras. In modern times, Poland is moving to the forefront of economic development and innovation, " +
                    "becoming an important player in the European Union. Poland's natural beauty encompasses the Carpathian Mount in the southwest, Masuria in the northeast, and the Baltic Sea coast in the north."
                },
            };

            await context.AddRangeAsync(countries);

            await context.SaveChangesAsync();
        }
    }
}
