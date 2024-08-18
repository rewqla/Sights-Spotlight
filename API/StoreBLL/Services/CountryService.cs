using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<CountryResponse>> GetAllCountries(CancellationToken cancellationToken = default)
        {
            var countries = await _countryRepository.GetAll();

            return _mapper.Map<IEnumerable<CountryResponse>>(countries);
        }

        public async Task<CountryDetailsResponse> GetCountryDetailsById(int id, CancellationToken cancellationToken = default)
        {
            var country = await _countryRepository.GetCountryByIdWithSights(id);

            return _mapper.Map<CountryDetailsResponse>(country);
        }

        public async Task<int> CreateCountry(CreateCountryRequest createCountry, CancellationToken cancellationToken = default)
        {
            var country = _mapper.Map<Country>(createCountry);
            await _countryValidator.ValidateAndThrowAsync(country);

            await _countryRepository.Add(country);
            await _countryRepository.Complete();

            return country.Id;

        }

        public async Task<bool> UpdateCountry(UpdateCountryRequest updateCountry, CancellationToken cancellationToken = default)
        {
            var country = await _countryRepository.FindById(updateCountry.Id);

            if (country == null)
            {
                return false;
            }

            var updatedCountry = _mapper.Map<Country>(updateCountry);

            await _countryValidator.ValidateAndThrowAsync(updatedCountry);

            _countryRepository.Update(country);
            await _countryRepository.Complete();

            return true;
        }

        public async Task<bool> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.FindById(id);

            if (country == null)
            {
                return false;
            }

            _countryRepository.Delete(country);
            await _countryRepository.Complete();

            return true;
        }
    }
}
