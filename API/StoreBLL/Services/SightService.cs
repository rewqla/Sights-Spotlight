using API.Contract.Requests;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Sight;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreBLL.Interfaces;
using StoreBLL.Mappers;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreBLL.Services
{
    public class SightService : ISightService
    {
        private readonly ISightRepository _sightRepository;
        private readonly ILogger<SightService> _logger;
        public SightService(ISightRepository sightRepository, ILogger<SightService> logger)
        {
            _sightRepository = sightRepository;
            _logger = logger;
        }

        public async Task<SightsResponse> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving all sights");

            var sights = await _sightRepository.GetAllSightsWithCountry(cancellationToken);

            var total = sights.Count();
            _logger.LogInformation("Fetched {Total} sights from the database", total);

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
            _logger.LogInformation("Sorted sights by: {SortBy}", request.SortBy);

            sights = sights
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);
            _logger.LogInformation("Paginated sights to Page: {Page}, PageSize: {PageSize}", request.Page, request.PageSize);

            var sightResponses = sights.Select(sight => new SightResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                Country = sight.Country.Name,
                YearOfFoundation = sight.YearOfFoundation,
                Description = sight.Description,
                Images = sight.SightPhotos.Select(photo => photo.Url).ToList()
            }).ToList();

            _logger.LogInformation("Returning {Count} sight responses", sightResponses.Count);


            return new SightsResponse
            {
                Items = sightResponses,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };
        }

        public async Task<int> GetCountAsync(string? country, int? yearOfFoundation, CancellationToken token = default)
        {
            _logger.LogInformation("Getting count");

            return await _sightRepository.GetCountAsync(country, yearOfFoundation, token);
        }
    }
}
