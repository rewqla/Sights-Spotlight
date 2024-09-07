using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses.Sight
{
    public record SightResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required List<string> Images { get; init; } = new();
        public required string Country { get; init; }
        public int? YearOfFoundation { get; init; }
        public required string Description { get; init; }
    }
}
