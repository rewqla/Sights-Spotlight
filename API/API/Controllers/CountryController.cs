using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.DTO;
using StoreBLL.Interfaces;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllCountries();
            return Ok(countries);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountryById(int id)
        {
            var country = await _countryRepository.GetCountryById(id);
            return Ok(country);
        }
    }
}
