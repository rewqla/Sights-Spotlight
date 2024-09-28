using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

namespace API.Contract.Requests.Country
{
    public record CreateCountryRequest
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string MainImageURL { get; init; }
        public required string SecondaryImageURL { get; init; }
        public required string Continent { get; init; }
    }
}
