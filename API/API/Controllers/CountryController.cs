using API.Routes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.DTO;
using StoreBLL.Interfaces;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

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
            var countries = await _countryService.GetCountries();

            return Ok(countries);
        }

        [HttpGet(CountryRoutes.GetById)]
        public async Task<ActionResult<IEnumerable<CountryDetailsDto>>> GetCountryById(int id)
        {
            var country = await _countryService.GetCountryDetails(id);

            return Ok(country);
        }
    }
}
