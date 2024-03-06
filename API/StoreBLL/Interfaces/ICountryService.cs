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
        Task<IEnumerable<CountryDto>> GetAllCountries();
        Task<CountryDetailsDto> GetCountryDetailsById(int id);
    }
}
