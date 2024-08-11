using StoreBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllCountries(CancellationToken cancellationToken = default);
        Task<CountryDetailsDto> GetCountryDetailsById(int id, CancellationToken cancellationToken = default);
        Task<int> CreateCountry(CountryCreateDto countryCreateDto, CancellationToken cancellationToken = default);
    }
}
