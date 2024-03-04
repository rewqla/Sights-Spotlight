using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using StoreDAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
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

            if (context.Countries.Any())
                return;

            var countries = new List<Country>
            {
                new Country
                {
                    Name = "Japan",
                    Description = "Japan is a country located on an archipelago in East Asia, defined by a peculiar mixture of modernity and tradition. It is known for its unique culture," +
                    " which includes tea drinking, ikebana (flower art), garden art and traditional noh theater. Japan is also famous for its innovations and technological achievements in" +
                    " the fields of automotive, electronics and robotics. The country has impressive natural beauty spots such as the Fuji Mountains and traditional spa resorts. Japan is a " +
                    "country with a rich history, unique traditions and a modern society.",
                    MainImgaeURL = "images/0883e295a40b9d76d22ca219c40328ef.jpeg",
                    SecondaryImageURL = "images/8eb35089104da5451605e8a0e26e3617.jpg",
                },

                new Country
                {
                    Name = "Germany",
                    Description = "Germany impresses with the diversity of its cultural heritage and its many attractions. The country is famous for its majestic art collections in museums " +
                    "such as the State Art Gallery in Dresden and the Museum der Ischluss in Munich. The literary heritage of Goethe and Schiller, theater performances in Bayreuth, and music " +
                    "festivals such as the Bayreuth Opera Festival define the German cultural scene. Historical landmarks such as Cologne Cathedral and Neuschwanstein Castle add to the country's " +
                    "unique character.",
                    MainImgaeURL = "images/image_processing20220209-4-14s88lt.jpeg",
                    SecondaryImageURL = "images/545c0345ecff036d6780ba816674b5d1.png",
                },

                new Country
                {
                    Name = "Italy",
                    Description = "A country with a rich history, from the Roman Empire to the Renaissance. Rome, as the capita is marked by ancient monuments. The culmination of art, architecture" +
                    " and cuisine. Recognized for its museums, opera and wine.Located on the Apennine Peninsula, with many outstanding natural places, including the Amalfi Coast and lakes. The world" +
                    " center of fashion and design.Cities such as Milan are famous for their shopping streets and prominent fashion events.",
                    MainImgaeURL = "images/d5ac9adb3358e6baddd6de118750adec.jpeg",
                    SecondaryImageURL = "images/2c490ac47e2d3ed9a95b6d499fb51767.png",
                },

                new Country
                {
                    Name = "Greece",
                    Description = "Located on the Balkan Peninsula, Greece is considered the cradle of ancient Greek civilization and democracy. It is noteworthy for its ancient monuments, such as the " +
                    "Acropolis in Athens, the Epidaurus Theater and the Temple of Olympia. The country is considered to be the cradle of ancient Greek mythology and philosophy, with prominent philosophers " +
                    "such as Socrates, Plato and Aristotle. Greek islands such as Crete, Santorini, and Mykonos are famous for their crystal water, traditional architecture, and sunny climate.",
                    MainImgaeURL = "images/27648e5b7bc2c3ebcad4e19cb8de16c8.jpg",
                    SecondaryImageURL = "images/e00fa62d1f1c5a3042fa6744c321ff02.png",
                },

                new Country
                {
                    Name = "Poland",
                    Description = "Located in the center of Europe, Poland has a rich cultural and historical past, the influence of which is e in its architecture, traditions and cuisine. Poland has " +
                    "witnessed great historical events, including the cre the Kingdom of Poland and its entry into the United Kingdom with Lithuania. Poland's castles and fortre such as Wawel in " +
                    "Krakow and Malbork in the northwest, bear witness to the greatness of bygone eras. In modern times, Poland is moving to the forefront of economic development and innovation, " +
                    "becoming an important player in the European Union. Poland's natural beauty encompasses the Carpathian Mount in the southwest, Masuria in the northeast, and the Baltic Sea coast in the north.",
                    MainImgaeURL = "images/84c05622078e18f717d0a37fea146e22.jpg",
                    SecondaryImageURL = "images/48d43817407ce4ab59051650d6597565.png",
                },
            };

            await context.AddRangeAsync(countries);

            var sights = new List<Sight>
            {
                new Sight
                {
                    Name = "MOUNT FUJI",
                    Description = "Mount Fuji is the tallest volcano in Japan and a symbol of the country. Its conical shape and height of 3,776 meters make it an impressive natural object. Climbing " +
                    "to the top of Mount Fuji is a popular tourist activity. Shrines and temples are located around the mountain, including Shrinto Temple and Fuji Sangu Temple. Mount Fuji has sacred " +
                    "significance for the Japanese and is a UNESCO World Heritage Site.",
                    CountryId = 1,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/c1958ee9fc8fa930ee50651e5092c47d.png",
                        }
                    },
                },
                new Sight
                {
                    Name = "KYOTO",
                    Description = "Kyoto, located on the island of Honshu in Japan, impresses with its atmosphere of tradition and beauty. The city has many historical temples  and shrines, among " +
                    "which are Kinkakuji (Golden Pavilion Temple) and Fushiminai (Interior Temple). Geisha traditions live on in the Gion neighborhood, and the Bamboo Forest in Arashiyama creates " +
                    "an impressive exotic landscape. The city's cultural heritage is strikingly unique, and the surrounding mountains add to the tranquil aura of this unique corner of Japan.",
                    CountryId = 1,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/c6a1ca680cfe5e37e15deb2e08ce5881.jpg",
                        },
                        new SightPhoto
                        {
                            Url = "images/1d938fd365b9826328394ea88f1deca5.jpg",
                        },
                        new SightPhoto
                        {
                            Url = "images/3bf4876ec4c927e86190734a12aa4d11.jpg",
                        },
                    },
                },
                new Sight
                {
                    Name = "Sensoji Temple in Nara",
                    Description = "The Sensoji Temple in Nara is a place where spirituality and art come together in a huge way. Its prominent bronze statue of the Great Buddha, more than 15 meters high, " +
                    "impresses with its majesty and craftsmanship. Founded in the 8th century, the temple is a UNESCO World Heritage Site and attracts believers and art lovers alike with its grandeur " +
                    "and spiritual atmosphere. Located in a picturesque park where wild deer roam freely, Sensoji Temple is a unique cultural treasure in Japan.",
                    CountryId = 1,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/deab2adb6dba626a4885157e62dc11e2.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/8fc19bea4f7230161f7152b9bb6dcf98.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/21945f39a32d131660e3ca44b3ef034c.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/f793bbe0754d71f32eb48ea661e68f46.png",
                        },
                    },
                },

                new Sight
                {
                    Name = "Kölner Dom",
                    Description = "Founded in the 13th century, the majestic Gothic cathedral is a symbol of Cologne. It was destroyed during the Second World War. Cologne Cathedral impresses " +
                    "with the majesty of its structure, over 150 meters high, and the impressive detail of its stained glass windows. It attracts millions of tourists who seek to enjoy its " +
                    "architectural beauty and location on the banks of the Rhine.",
                    CountryId = 2,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/59fbcdcf2b914a2bc302b87f0454a6c3.png",
                        }
                    },
                },
                new Sight
                {
                    Name = "Heidelberg",
                    Description = "The city, whose history dates back more than 800 years, retains traces of the Middle Ages and the Renaissance.Heidelberg Castle, built in the 13th century, has " +
                    "become a symbol of the city. It was a place of creativity for famous writers, including Goethe and Schiller, who were inspired by the city's beauty. The picturesque banks of " +
                    "the Neckar River, as well as the townhouses with their charming streets, create a unique atmosphere of the city.It attracts tourists with its history, impressive architectural " +
                    "ensemble and location on the river bank.",
                    CountryId = 2,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/2ed319f0746aa21258e6048955f14911.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/7a0431013a63cd0dda107a1525ce92aa.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/75551c823b983f909344d9a1f1c8ecf8.png",
                        },
                    },
                },
                new Sight
                {
                    Name = "Sensoji Temple in Nara",
                    Description = "It is located in the Bavarian Alps, surrounded by picturesque landscapes and lakes, which gives it a romantic character. It became the prototype for the " +
                    "Sleeping Beauty Castle in Disneyland.Recognized as one of the most beautiful castles in the world, it impresses with its grandeur and fantasy.The castle impresses with " +
                    "its unique design, exterior and interior details that make it an outstanding architectural object.",
                    CountryId = 2,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/a31e1ffade9dca616c958b3f9ad927f7.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/84b4ec661df5d6407cf5da9583664d69.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/891333bbd6b5ab1db2868e654188c2c0.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/a5359dc97e2d7ed1d4bd31fc142cc73d.png",
                        },
                    },
                },

                new Sight
                {
                    Name = "Coliseum",
                    Description = "A symbol of the Roman Empire and an architectural masterpiece built in the 1st century AD for gladiatorial games and various events. The oval arena could " +
                    "accommodate more than 50 thousand spectators and has four floors of colonnades decorated with Doric, Ionic and Corinthian porticoes. ",
                    CountryId = 3,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/3de1707f01fa3507f8e0dfb454b95266.png",
                        }
                    },
                },
                new Sight
                {
                    Name = "Heidelberg",
                    Description = "It was built in the Gothic style and impresses with its grandeur and detailed architectural forms. It is defined by a huge dome designed by Brunelleschi. " +
                    "The interior decoration includes works by such masters as Giotto, Vasari and Zuccari. Masterpieces of painting and sculpture adorn the chapels and canopies.",
                    CountryId = 3,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/20b8d660c4ce42b62f4ec9f5ded96a2f.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/4b88dbabfe71e1a1f649d92819a5742e.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/55140bbaea8ab21aa13de4ef4d090e73.png",
                        },
                    },
                },
                new Sight
                {
                    Name = "Venice",
                    Description = "Located on the lagoon of the Adriatic Sea, Venice is famous for its architecture and the system of canals that serve as streets.Many bridges, including " +
                    "the prominent Rialto and Accademia bridges, connect the picturesque streets, and the canals serve as the main arteries of the city.Preserving traces of its majestic " +
                    "past, Venice was the cradleof the republic and a huge trading center in the Middle Ages.",
                    CountryId = 3,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/b0f14fcdf76edffca2215258b0d7b30c.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/3816533f9b96f639821e66ffe668705e.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/27f6ab3532ba84f59d0795aadda2d3ef.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/0123cd07f5ed2816b49904cceb4cbeba.png",
                        },
                    },
                },

                new Sight
                {
                    Name = "Acropolis",
                    Description = "A modern museum exhibiting thousands of artifacts found on the Acropolis, including fragments from the Parthenon and statues. The Acropolis served as a " +
                    "symbol of democracy and expressed cultural and religious importance for the ancient Athenian citizens. The most prominent temple was built in the 5th century BC for " +
                    "the goddess Athena. It became a symbol of classical ancient Greek architecture and a real gem of the Acropolis.",
                    CountryId = 4,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/b09aa7293d2b098915254edfba9005cf.png",
                        }
                    },
                },
                new Sight
                {
                    Name = "Meteors",
                    Description = "Meteors is a unique complex of rocks and vertical pillars, where monasteries founded in the Middle Ages are located hundreds of meters " +
                    "high. Located in the heart of the Tesalian snail, the Meteors impress with breathtaking views and an unsurpassed landscape. The stone pillars that " +
                    "stand out for their height and shapes are a natural wonder and create incredible landscapes.",
                    CountryId = 4,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/884f7173ef0663e5bfebc95daab8a852.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/8bf5f57dbf5bc222518b5709053bb712.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/bea1aabf70563ac715ea6d9970b014c1.png",
                        },
                    },
                },
                new Sight
                {
                    Name = "Acropolis",
                    Description = "The Acropolis is a majestic fortress and architectural complex that stands on a high hill, becoming a symbol of ancient Greek civilizations. " +
                    "The modern museum, which exhibits artifacts and sculptures found on the territory of the Acropolis, complements the understanding of its history. The " +
                    "Acropolis served as a rallying point for the citizens of Athens, where state and religious affairs were resolved.",
                    CountryId = 4,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/46740256de52c8e0f4a89d8a06dc3adb.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/e14ba1a1d1fcbf9a4a489589b82c99ec.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/8e968adcb2433f338b5a217f0492784e.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/86d59db97636c01986329e1faa805461.png",
                        },
                    },
                },

                new Sight
                {
                    Name = "Wawel",
                    Description = "Located on a hilltop in the center of Krakow, Wawel is one of the most important historical and cultural attractions in Poland. The complex, which " +
                    "includes the Royal Castle and the Cathedral, has become one of the residences of Polish kings and a place of coronations and burials. The Cathedral of St. " +
                    "Stanislaus and St. Wenceslas: The site of coronations and burials of Polish rulers, with the impressive Sigismund Chapel. Wawel Collection: A museum featuring " +
                    "masterpieces of Polish art, royal relics and valuables.",
                    CountryId = 5,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/ee9d7df487be8e15f9c346cab2da42e5.png",
                        }
                    },
                },
                new Sight
                {
                    Name = "Old Town",
                    Description = "The Old Town is a reflection of the restored heart of Warsaw, which was almost completely destroyed during World War II but was rebuilt with great " +
                    "enthusiasm in the Renaissance and Baroque styles. The Old Town Market is a colorful and picturesque place where various events and festivals take place. The Royal " +
                    "Castle is a fortress reconstructed in the historical style, impresses with its architecture and is a venue for cultural events.",
                    CountryId = 5,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/40dbc65daac4ad980d4820284bdbd0d8.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/828f598670fce50e3725a58bfa36430b.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/e195696b09e1f77b2fa2aaf3901e3737.png",
                        },
                    },
                },
                new Sight
                {
                    Name = "The Palace",
                    Description = "The building has multimedia capabilities and includes theaters, concert halls, exhibition spaces, as well as cultural and scientific  institutions. " +
                    "The Palace often serves as a venue for various cultural  events, exhibitions, concerts, festivals and scientific conferences.The complex houses research " +
                    "institutes, libraries and educational institutions, making it an important center for the scientific  community.",
                    CountryId = 5,
                    SightPhotos = new List<SightPhoto>
                    {
                        new SightPhoto
                        {
                            Url = "images/95aa106eb581471627d5d9055636839f.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/e864315c72ccac147a2e20e945a2ab93.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/9d6c82819047bc4d039854a1085d2e47.png",
                        },
                        new SightPhoto
                        {
                            Url = "images/9cfadb692132eb78da0a83ca276c6fdf.png",
                        },
                    },
                },
            };

            await context.AddRangeAsync(sights);

            await context.SaveChangesAsync();
        }
    }
}
