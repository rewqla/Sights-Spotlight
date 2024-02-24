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
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _countryService.GetCountries();

            return Ok(countries);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CountryDetailsDto>>> GetCountryById(int id)
        {
            var country = await _countryService.GetCountryDetails(id);

            return Ok(country);
        }
    }
}
