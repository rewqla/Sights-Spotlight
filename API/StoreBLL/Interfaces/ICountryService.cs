using API.Contract.Requests;
using API.Contract.Requests.Country;
using API.Contract.Responses.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetAllCountries(CancellationToken cancellationToken = default);
        Task<CountryDetailsResponse?> GetCountryDetailsById(int id, CancellationToken cancellationToken = default);
        Task<int> CreateCountry(CreateCountryRequest createCountry, CancellationToken cancellationToken = default);
        Task<bool> UpdateCountry(UpdateCountryRequest updateCountry, CancellationToken cancellationToken = default);
        Task<bool> DeleteCountry(int id, CancellationToken cancellationToken = default);
    }
}
