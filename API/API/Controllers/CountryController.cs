using API.Routes;
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
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _countryService.GetAllCountries();

            return Ok(countries);
        }

        [HttpGet(CountryRoutes.GetById)]
        public async Task<ActionResult<IEnumerable<CountryDetailsDto>>> GetCountryById(int id)
        {
            var country = await _countryService.GetCountryDetailsById(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }
    }
}
