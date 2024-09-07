using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses.Country
{
    public record CountryDetailsResponse : CountryResponse
    {
        public required string Description { get; init; }
        public List<CountrySightResponse> CountrySights { get; init; } = new();
    }
}
