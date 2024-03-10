using Newtonsoft.Json;
using NUnit.Framework;
using SightsSpotlight.Test.IntegrationTests;
using StoreBLL.DTO;
using System.Net;
using FluentAssertions;
using StoreDAL.Entities;

namespace SightsSpotlight.Test.Api.Test
{
    public class CountryIntegrationTest
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _httpClient;
        private const string RequestUri = "api/country/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _httpClient = _factory.CreateClient();
        }

        [Test]
        public async Task CountryController_GetAll_ReturnsOK()
        {
            //arrange
            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri);

            // assert
            httpResponse.EnsureSuccessStatusCode();

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsOK()
        {
            //arrange
            var countryId = 3;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            httpResponse.EnsureSuccessStatusCode();

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsNotFound()
        {
            //arrange
            var countryId = 1099;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task CountryController_GetAll_ReturnsAllCountryModels()
        {
            //arrange
            var expected = CountryModels.ToList();

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<CountryDto>>(stringResponse).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsCountryDetailsModel()
        {
            //arrange
            var expected = CountryDetailsModel;
            var countryId = 3;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<CountryDetailsDto>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsCountryDetailsModelWithIncludes()
        {
            //arrange
            var countryId = 3;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<CountryDetailsDto>(stringResponse);

            actual.CountrySightDtos.Should().NotBeEmpty();
        }

        public IEnumerable<CountryDto> CountryModels =
            new List<CountryDto>()
            {
                new CountryDto { Id = 1, Name = "Japan", ImageURL = "images/0883e295a40b9d76d22ca219c40328ef.jpg"},
                new CountryDto { Id = 2, Name = "Germany", ImageURL = "images/image_processing20220209-4-14s88lt.jpg"},
                new CountryDto { Id = 3, Name = "Italy", ImageURL = "images/d5ac9adb3358e6baddd6de118750adec.jpg"},
            };

        public CountryDetailsDto CountryDetailsModel = new CountryDetailsDto
        {
            Id = 3,
            Name = "Italy",
            ImageURL = "images/2c490ac47e2d3ed9a95b6d499fb51767.png",
            Description = "A country with a rich history, from the Roman Empire to the Renaissance. Rome, as the capita is marked by ancient monuments. " +
            "The culmination of art, architecture and cuisine. Recognized for its museums, opera and wine.Located on the Apennine Peninsula, with many " +
            "outstanding natural places, including the Amalfi Coast and lakes. The world center of fashion and design.Cities such as Milan are famous " +
            "for their shopping streets and prominent fashion events.",
            CountrySightDtos = new List<CountrySightDto>()
            {
               new CountrySightDto
               {
                    Id=1,
                    Name="Coliseum",
                    Description="A symbol of the Roman Empire and an architectural masterpiece built in the 1st century AD for gladiatorial games and various events. " +
                    "The oval arena could accommodate more than 50 thousand spectators and has four floors of colonnades decorated with Doric, Ionic and Corinthian porticoes. ",
                    ImageURLs= new List<string>()
                    {
                        "images/3de1707f01fa3507f8e0dfb454b95266.png"
                    }
               },
               new CountrySightDto
               {
                    Id=2,
                    Name = "Heidelberg",
                    Description = "It was built in the Gothic style and impresses with its grandeur and detailed architectural forms. It is defined by a huge dome designed by Brunelleschi. " +
                    "The interior decoration includes works by such masters as Giotto, Vasari and Zuccari. Masterpieces of painting and sculpture adorn the chapels and canopies.",
                    ImageURLs = new List<string>
                    {
                        "images/20b8d660c4ce42b62f4ec9f5ded96a2f.png",
                        "images/4b88dbabfe71e1a1f649d92819a5742e.png",
                        "images/55140bbaea8ab21aa13de4ef4d090e73.png",
                    },
               },
               new CountrySightDto
               {
                    Id=3,
                    Name = "Venice",
                    Description = "Located on the lagoon of the Adriatic Sea, Venice is famous for its architecture and the system of canals that serve as streets.Many bridges, including " +
                    "the prominent Rialto and Accademia bridges, connect the picturesque streets, and the canals serve as the main arteries of the city.Preserving traces of its majestic " +
                    "past, Venice was the cradleof the republic and a huge trading center in the Middle Ages.",
                    ImageURLs = new List<string>
                    {
                        "images/b0f14fcdf76edffca2215258b0d7b30c.png",
                        "images/3816533f9b96f639821e66ffe668705e.png",
                        "images/27f6ab3532ba84f59d0795aadda2d3ef.png",
                        "images/0123cd07f5ed2816b49904cceb4cbeba.png",
                    },
               }
            }
        };


        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _httpClient.Dispose();
        }
    }
}
