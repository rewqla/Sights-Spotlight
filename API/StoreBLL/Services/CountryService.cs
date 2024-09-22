using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using FluentValidation;
using Microsoft.Extensions.Logging;
using StoreBLL.Exceptions;
using StoreBLL.Interfaces;
using StoreBLL.Mappers;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
    public class CountryService(
        ICountryRepository countryRepository,
        IValidator<Country> countryValidator,
        ILogger<CountryService> logger)
        : ICountryService
    {
        public async Task<IEnumerable<CountryResponse>> GetAllCountries(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving all countries from the repository.");
            var countries = await countryRepository.GetAll(cancellationToken);

            if (!countries.Any())
            {
                logger.LogWarning("No countries found in repository.");
                throw new CountryException("No countries found.");
            }

            return countries.Select(x => CountryMappingExtensions.MapToCountryResponse(x)).ToList();
        }

        public async Task<CountryDetailsResponse?> GetCountryDetailsById(int id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                logger.LogInformation("Retrieving country details for ID {CountryId}.", id);
                var country = await countryRepository.GetCountryByIdWithSights(id, cancellationToken);

                if (country == null)
                {
                    logger.LogWarning("Country with ID {CountryId} not found in repository.", id);
                    throw new CountryNotFoundException(id);
                }

                return CountryMappingExtensions.MapToCountryDetailsResponse(country);
            }
            catch (CountryNotFoundException ex)
            {
                return null;
            }
        }

        public async Task<int> CreateCountry(CreateCountryRequest createCountry,
            CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Creating a new country with name {CountryName}.", createCountry.Name);

            var country = CountryMappingExtensions.MapToCountry(createCountry);

           await countryValidator.ValidateAndThrowAsync(country, cancellationToken);

            await countryRepository.Add(country!, cancellationToken);
            await countryRepository.Complete();

            return country.Id;
        }

        public async Task<bool> UpdateCountry(UpdateCountryRequest updateCountry,
            CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Updating country with ID {CountryId}.", updateCountry.Id);

            var country = await countryRepository.FindById(updateCountry.Id, cancellationToken);

            if (country == null)
            {
                logger.LogWarning("Country with ID {CountryId} not found.", updateCountry.Id);
                throw new CountryNotFoundException(updateCountry.Id);
            }

            CountryMappingExtensions.UpdateCountryFromRequest(country, updateCountry);

            await countryValidator.ValidateAndThrowAsync(country, cancellationToken);

            countryRepository.Update(country);
            await countryRepository.Complete();

            return true;
        }

        public async Task<bool> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting country with ID {CountryId}.", id);

            var country = await countryRepository.FindById(id, cancellationToken);

            if (country == null)
            {
                logger.LogWarning("Country with ID {CountryId} not found.", id);
                throw new CountryNotFoundException(id);
            }

            countryRepository.Delete(country);
            await countryRepository.Complete();

            return true;
        }
    }
}