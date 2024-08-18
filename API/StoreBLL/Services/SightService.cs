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

        public async Task<IEnumerable<SightsResponse>> GetAllSights(GetAllSightsRequest request, CancellationToken cancellationToken = default)
        {
            var sights = await _sightRepository.GetAllSightsWithCountry(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Country))
            {
                sights = sights.Where(sight => sight.Country.Name.Contains(request.Country, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                sights = sights.Where(sight => sight.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
            }

            return sights.Select(sight => new SightsResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                Country = sight.Country.Name,
                Description = sight.Description,
                Images = sight.SightPhotos.Select(photo => photo.Url).ToList()
            }).ToList();
        }
    }
}
