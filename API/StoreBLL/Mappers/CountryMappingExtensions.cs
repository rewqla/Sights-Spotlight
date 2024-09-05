using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Mappers
{
    public static class CountryMappingExtensions
    {
        public static CountryResponse MapToCountryResponse(Country country)
        {
            return new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
                ImageURL = country.MainImageURL,
            };
        }
        public static CountryDetailsResponse MapToCountryDetailsResponse(Country country)
        {
            return new CountryDetailsResponse
            {
                Id = country.Id,
                Name = country.Name,
                ImageURL = country.MainImageURL,
                Description = country.Description,
                CountrySights = country.Sights.Select(MapToCountrySightResponse).ToList()
            };
        }

        public static CountrySightResponse MapToCountrySightResponse(Sight sight)
        {
            return new CountrySightResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                ImageURLs = sight.SightPhotos.Select(x => x.Url).ToList(),
                Description = sight.Description,
            };
        }

        public static void UpdateCountryFromRequest(Country existingCountry, UpdateCountryRequest updateCountryRequest)
        {
            existingCountry.Name = updateCountryRequest.Name;
            existingCountry.Description = updateCountryRequest.Description;
            existingCountry.MainImageURL = updateCountryRequest.MainImageURL;
            existingCountry.SecondaryImageURL = updateCountryRequest.SecondaryImageURL;
            existingCountry.Sights = new List<Sight>();
        }

        public static Country MapToCountry(CreateCountryRequest createCountryRequest)
        {
            return new Country
            {
                Name = createCountryRequest.Name,
                Description = createCountryRequest.Description,
                MainImageURL = createCountryRequest.MainImageURL,
                SecondaryImageURL = createCountryRequest.SecondaryImageURL,
                Sights = new List<Sight>()
            };
        }
    }
}
