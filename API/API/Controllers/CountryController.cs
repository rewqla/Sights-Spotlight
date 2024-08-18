using API.Authorization;
using API.Contract.Requests;
using API.Contract.Responses;
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
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet(CountryRoutes.GetAll)]
        public async Task<ActionResult<IAsyncEnumerable<CountryResponse>>> GetCountries(CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetAllCountries(cancellationToken);

            return Ok(countries);
        }

        [HttpGet(CountryRoutes.GetById)]
        [Authorize(PolicyRoles.Member)]
        public async Task<ActionResult<IAsyncEnumerable<CountryDetailsResponse>>> GetCountryById(int id, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryDetailsById(id, cancellationToken);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost(CountryRoutes.Create)]
        [Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest createCountry, CancellationToken cancellationToken)
        {
            int createdCounryId = await _countryService.CreateCountry(createCountry, cancellationToken);

            return Ok(createdCounryId);
        }

        [HttpPut(CountryRoutes.Update)]
        [Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryRequest updateCountry, CancellationToken cancellationToken)
        {
            var result = await _countryService.UpdateCountry(updateCountry, cancellationToken);

            if (!result)
            {
                return NotFound();
            }

            return NoContent(); 
        }

        [HttpDelete(CountryRoutes.Delete)]
        [Authorize(PolicyRoles.Admin)]
        public async Task<IActionResult> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            var result = await _countryService.DeleteCountry(id, cancellationToken);

            if (!result)
            {
                return NotFound(new { message = "Country not found" });
            }

            return NoContent(); 
        }
    }
}