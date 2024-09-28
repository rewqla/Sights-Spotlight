using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses.Country
{
    public record CountryResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string ImageURL { get; init; }
        public required string Continent { get; init; }
    }
}
