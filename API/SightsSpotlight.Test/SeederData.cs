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
                }
            );

            context.Sights.AddRange(
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
                }
            );

            context.SaveChanges();
        }
    }
}
