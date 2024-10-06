using API.Contract.Requests.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Requests.Sight
{
    public record GetAllSightsRequest : PagedRequest
    {
        public string? Country { get; init; }
        public int? YearOfFoundationFrom { get; init; }
        public int? YearOfFoundationTo { get; init; }
        public string? SortBy { get; init; }
    }
}
