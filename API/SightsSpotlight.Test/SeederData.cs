using StoreDAL.Data;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SightsSpotlight.Test
{
    public class SeederData
    {
        public static void Seed(StoreContext context)
        {
            context.Countries.AddRange(
                new Country
                {
                    Name = "Japan",
                    Description = "Japan is a country located on an archipelago in East Asia, defined by a peculiar mixture of modernity and tradition. It is known for its unique culture," +
                    " which includes tea drinking, ikebana (flower art), garden art and traditional noh theater. Japan is also famous for its innovations and technological achievements in" +
                    " the fields of automotive, electronics and robotics. The country has impressive natural beauty spots such as the Fuji Mountains and traditional spa resorts. Japan is a " +
                    "country with a rich history, unique traditions and a modern society.",
                    MainImgaeURL = "images/0883e295a40b9d76d22ca219c40328ef.jpg",
                    SecondaryImageURL = "images/8eb35089104da5451605e8a0e26e3617.jpg",
                },

                new Country
                {
                    Name = "Germany",
                    Description = "Germany impresses with the diversity of its cultural heritage and its many attractions. The country is famous for its majestic art collections in museums " +
                    "such as the State Art Gallery in Dresden and the Museum der Ischluss in Munich. The literary heritage of Goethe and Schiller, theater performances in Bayreuth, and music " +
                    "festivals such as the Bayreuth Opera Festival define the German cultural scene. Historical landmarks such as Cologne Cathedral and Neuschwanstein Castle add to the country's " +
                    "unique character.",
                    MainImgaeURL = "images/image_processing20220209-4-14s88lt.jpg",
                    SecondaryImageURL = "images/545c0345ecff036d6780ba816674b5d1.png",
                },

                new Country
                {
                    Name = "Italy",
                    Description = "A country with a rich history, from the Roman Empire to the Renaissance. Rome, as the capita is marked by ancient monuments. The culmination of art, architecture" +
                    " and cuisine. Recognized for its museums, opera and wine.Located on the Apennine Peninsula, with many outstanding natural places, including the Amalfi Coast and lakes. The world" +
                    " center of fashion and design.Cities such as Milan are famous for their shopping streets and prominent fashion events.",
                    MainImgaeURL = "images/d5ac9adb3358e6baddd6de118750adec.jpg",
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
                }
            );

            context.SaveChanges();
        }
    }
}
