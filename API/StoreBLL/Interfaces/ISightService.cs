using API.Contract.Requests;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Sight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
    public interface ISightService
    {
        Task<SightsResponse> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(string? country, int? yearOfFoundation, CancellationToken token = default);
    }
}
