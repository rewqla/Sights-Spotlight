using API.Contract.Responses.Country;
using API.Contract.Responses.Sight;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Mappers
{
    public static class SightsMappingExtensions
    {
        public static SightResponse MapToSightResponse(Sight sight)
        {
            return new SightResponse
            {
                Id = sight.Id,
                Name = sight.Name,
                Description = sight.Description,
                YearOfFoundation = sight.YearOfFoundation,
                Country = sight.Country.Name,
                Images = sight.SightPhotos.Select(x => x.Url).ToList(),
            };
        }
    }
}
