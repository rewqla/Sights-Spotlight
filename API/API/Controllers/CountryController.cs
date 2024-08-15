using API.Authorization;
using API.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.DTO;
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
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries(CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetAllCountries(cancellationToken);

            return Ok(countries);
        }

        [HttpGet(CountryRoutes.GetById)]
        public async Task<ActionResult<IEnumerable<CountryDetailsDto>>> GetCountryById(int id, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryDetailsById(id, cancellationToken);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost(CountryRoutes.Create)]
        //[Authorize(PolicyRoles.Admin)]
        public async Task<ActionResult> CreateCountry([FromBody] CountryCreateDto countryCreateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int createdCounryId = await _countryService.CreateCountry(countryCreateDto, cancellationToken);

            return Ok(createdCounryId);
        }
    }
}

//update
//Request reponse conract
//advnced