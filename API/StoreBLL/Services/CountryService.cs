using AutoMapper;
using StoreBLL.DTO;
using StoreBLL.Interfaces;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreBLL.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountries()
        {
            var countries = await _countryRepository.GetAll();

            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDetailsDto> GetCountryDetailsById(int id)
        {
            var country = await _countryRepository.GetCountryByIdWithSights(id);

            return _mapper.Map<CountryDetailsDto>(country);
        }

        public async Task<int> CreateCountry(CountryCreateDto countryCreateDto)
        {
            try
            {
                var country = _mapper.Map<Country>(countryCreateDto);

                await _countryRepository.Add(country);
                await _countryRepository.Complete();

                return country.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the country.", ex);
            }
        }
    }
}
