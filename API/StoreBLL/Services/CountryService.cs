using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<Country> _countryValidator;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper, IValidator<Country> countryValidator)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _countryValidator = countryValidator;
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountries(CancellationToken cancellationToken = default)
        {
            var countries = await _countryRepository.GetAll();

            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDetailsDto> GetCountryDetailsById(int id, CancellationToken cancellationToken = default)
        {
            var country = await _countryRepository.GetCountryByIdWithSights(id);

            return _mapper.Map<CountryDetailsDto>(country);
        }

        public async Task<int> CreateCountry(CountryCreateDto countryCreateDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var country = _mapper.Map<Country>(countryCreateDto);
                await _countryValidator.ValidateAndThrowAsync(country);

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
