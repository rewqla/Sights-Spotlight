using API.Contract.Requests.Sight;
using API.Contract.Responses.Sight;

namespace StoreBLL.Interfaces
{
    public interface ISightService
    {
        Task<SightsResponse> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(string? country, int? yearOfFoundation, CancellationToken token = default);
    }
}
