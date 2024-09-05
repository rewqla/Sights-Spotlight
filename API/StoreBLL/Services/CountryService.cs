using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using StoreBLL.Interfaces;
using StoreBLL.Mappers;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System.Diagnostics.Metrics;

namespace StoreBLL.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IValidator<Country> _countryValidator;
        private readonly ILogger<CountryService> _logger;
        public CountryService(ICountryRepository countryRepository, IValidator<Country> countryValidator, ILogger<CountryService> logger = null)
        {
            _countryRepository = countryRepository;
            _countryValidator = countryValidator;
            _logger = logger;
        }

        public async Task<IEnumerable<CountryResponse>> GetAllCountries(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving all countries from the repository.");
            var countries = await _countryRepository.GetAll();

            return countries.Select(x => CountryMappingExtensions.MapToCountryResponse(x)).ToList();
        }

        public async Task<CountryDetailsResponse> GetCountryDetailsById(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving country details for ID {CountryId}.", id);
            var country = await _countryRepository.GetCountryByIdWithSights(id);

            return CountryMappingExtensions.MapToCountryDetailsResponse(country);
        }

        public async Task<int> CreateCountry(CreateCountryRequest createCountry, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating a new country with name {CountryName}.", createCountry.Name);

            var country = CountryMappingExtensions.MapToCountry(createCountry);
            await _countryValidator.ValidateAndThrowAsync(country);

            await _countryRepository.Add(country);
            await _countryRepository.Complete();

            return country.Id;
        }

        public async Task<bool> UpdateCountry(UpdateCountryRequest updateCountry, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating country with ID {CountryId}.", updateCountry.Id);

            var country = await _countryRepository.FindById(updateCountry.Id);

            if (country == null)
            {
                return false;
            }

            CountryMappingExtensions.UpdateCountryFromRequest(country, updateCountry);

            await _countryValidator.ValidateAndThrowAsync(country);

            _countryRepository.Update(country);
            await _countryRepository.Complete();

            return true;
        }

        public async Task<bool> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting country with ID {CountryId}.", id);

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
