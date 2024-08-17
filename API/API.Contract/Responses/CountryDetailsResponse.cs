using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses
{
    public class CountryDetailsResponse : CountryResponse
    {
        public required string Description { get; set; }
        public List<CountrySightResponse> CountrySights { get; set; }
    }
}
