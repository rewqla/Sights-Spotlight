using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Contract.Requests.Country;

namespace API.Contract.Requests
{
    public record UpdateCountryRequest : CreateCountryRequest
    {
        public required int Id { get; init; }
    }
}
