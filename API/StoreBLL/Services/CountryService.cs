using StoreBLL.DTO;
using StoreBLL.Interfaces;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            var countries = await _countryRepository.GetAll();

            return countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                ImgaeURL = c.MainImgaeURL
            });
        }

        public async Task<CountryDetailsDto> GetCountryDetails(int id)
        {
            var country = await _countryRepository.GetCountryById(id);

            if (country == null)
                return null;

            var countryDto = new CountryDetailsDto
            {
                Id = country.Id,
                Name = country.Name,
                ImgaeURL = country.SecondaryImageURL,
                Description = country.Description,
                CountrySightDtos = country.Sights.Select(s => new CountrySightDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    ImageURLs = s.SightPhotos.Select(p => p.Url).ToList()
                }).ToList()
            };

            return countryDto;
        }
    }
}
