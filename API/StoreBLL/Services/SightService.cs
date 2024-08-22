using API.Contract.Requests;
using API.Contract.Requests.Sight;
using API.Contract.Responses.Sight;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StoreBLL.Interfaces;
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
        private readonly IMapper _mapper;
        public SightService(ISightRepository sightRepository, IMapper mapper)
        {
            _sightRepository = sightRepository;
            _mapper = mapper;
        }

        public async Task<SightsResponse> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default)
        {
            var sights = await _sightRepository.GetAllSightsWithCountry(cancellationToken);

            var total = await _sightRepository.GetCountAsync(request.Country, request.YearOfFoundationFrom, cancellationToken);

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

            sights = sights
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var sightResponses = sights.Select(sight => new SightResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                Country = sight.Country.Name,
                YearOfFoundation = sight.YearOfFoundation,
                Description = sight.Description,
                Images = sight.SightPhotos.Select(photo => photo.Url).ToList()
            }).ToList();

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
            return await _sightRepository.GetCountAsync(country, yearOfFoundation, token);
        }
    }
}
