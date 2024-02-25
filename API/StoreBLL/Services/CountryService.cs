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

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            var countries = await _countryRepository.GetAll();

            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDetailsDto> GetCountryDetails(int id)
        {
            var country = await _countryRepository.GetCountryById(id);

            if (country == null)
                return null;

            return _mapper.Map<CountryDetailsDto>(country);
        }
    }
}
