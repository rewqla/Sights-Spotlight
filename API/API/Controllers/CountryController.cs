using API.Authorization;
using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using API.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.Interfaces;


namespace API.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<SightController> _logger;
        public CountryController(ICountryService countryService, ILogger<SightController> logger) 
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet(CountryRoutes.GetAll)]
        public async Task<ActionResult<IAsyncEnumerable<CountryResponse>>> GetCountries(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all countries.");

            var countries = await _countryService.GetAllCountries(cancellationToken);
            _logger.LogInformation("Fetched {Count} countries.", countries.Count());

            return Ok(countries);
        }

        [HttpGet(CountryRoutes.GetById)]
        [Authorize(PolicyRoles.Admin)]
        //[Authorize(PolicyRoles.Member)]
        public async Task<ActionResult<IAsyncEnumerable<CountryDetailsResponse>>> GetCountryById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching details for country with ID {CountryId}.", id);
            var country = await _countryService.GetCountryDetailsById(id, cancellationToken);

            if (country == null)
            {
                _logger.LogWarning("Country with ID {CountryId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Country with ID {CountryId} fetched successfully.", id);
            return Ok(country);
        }

        [HttpPost(CountryRoutes.Create)]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        //[Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest createCountry, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new country with name {CountryName}.", createCountry.Name);

            int createdCountryId = await _countryService.CreateCountry(createCountry, cancellationToken);
            _logger.LogInformation("Country created successfully with ID {CountryId}.", createdCountryId);

            return Ok(createdCountryId);
        }

        [HttpPut(CountryRoutes.Update)]
        [Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryRequest updateCountry, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating country with ID {CountryId}.", updateCountry.Id);

            var result = await _countryService.UpdateCountry(updateCountry, cancellationToken);

            if (!result)
            {
                _logger.LogWarning("Country with ID {CountryId} not found for update.", updateCountry.Id);
                return NotFound();
            }

            _logger.LogInformation("Country with ID {CountryId} updated successfully.", updateCountry.Id);
            return NoContent(); 
        }

        [HttpDelete(CountryRoutes.Delete)]
        [Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting country with ID {CountryId}.", id);

            var result = await _countryService.DeleteCountry(id, cancellationToken);

            if (!result)
            {
                _logger.LogWarning("Country with ID {CountryId} not found for deletion.", id);
                return NotFound(new { message = "Country not found" });
            }

            _logger.LogInformation("Country with ID {CountryId} deleted successfully.", id);
            return NoContent(); 
        }
    }
}