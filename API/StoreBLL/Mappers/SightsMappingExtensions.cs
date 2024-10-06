using API.Contract.Responses.Sight;
using StoreDAL.Entities;

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
