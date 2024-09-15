using API.Contract.Requests.General;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Sight;
using Microsoft.Extensions.Logging;
using StoreBLL.Interfaces;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
    public class SightService(ISightRepository sightRepository, ILogger<SightService> logger)
        : ISightService
    {
        public async Task<SightsResponse> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Retrieving all sights");

            var sights = await sightRepository.GetAllSightsWithCountry(cancellationToken);

            var total = sights.Count();
            logger.LogInformation("Fetched {Total} sights from the database", total);

            if (!string.IsNullOrWhiteSpace(request.Country))
            {
                sights = sights.Where(sight => sight.Country.Name.Contains(request.Country, StringComparison.OrdinalIgnoreCase));
            }

            if (request.YearOfFoundationFrom.HasValue)
            {
                sights = sights.Where(sight => sight.YearOfFoundation >= request.YearOfFoundationFrom.Value);
            }

            if (request.YearOfFoundationTo.HasValue)
            {
                sights = sights.Where(sight => sight.YearOfFoundation <= request.YearOfFoundationTo.Value);
            }

            sights = request.SortBy switch
            {
                "Country" => sights.OrderBy(sight => sight.Country.Name),
                "-Country" => sights.OrderByDescending(sight => sight.Country.Name),
                "YearOfFoundation" => sights.OrderBy(sight => sight.YearOfFoundation),
                "-YearOfFoundation" => sights.OrderByDescending(sight => sight.YearOfFoundation),
                _ => sights
            };
            logger.LogInformation("Sorted sights by: {SortBy}", request.SortBy);

            sights = sights
                .Skip((request.Page.GetValueOrDefault(PagedRequest.DefaultPage) - 1) * request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize))
                .Take(request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize));
            logger.LogInformation("Paginated sights to Page: {Page}, PageSize: {PageSize}", request.Page, request.PageSize);

            var sightResponses = sights.Select(sight => new SightResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                Country = sight.Country.Name,
                YearOfFoundation = sight.YearOfFoundation,
                Description = sight.Description,
                Images = sight.SightPhotos.Select(photo => photo.Url).ToList()
            }).ToList();

            logger.LogInformation("Returning {Count} sight responses", sightResponses.Count);


            return new SightsResponse
            {
                Items = sightResponses,
                Page = request.Page.GetValueOrDefault(PagedRequest.DefaultPage),
                PageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize),
                Total = total
            };
        }

        public async Task<int> GetCountAsync(string? country, int? yearOfFoundation, CancellationToken token = default)
        {
            logger.LogInformation("Getting count");

            return await sightRepository.GetCountAsync(country, yearOfFoundation, token);
        }
    }
}
