using API.Contract.Requests;
using API.Contract.Responses;
using AutoMapper;
using Microsoft.Extensions.Logging;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, CountryResponse>()
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.MainImageURL));

            CreateMap<Country, CountryDetailsResponse>()
                   .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.SecondaryImageURL))
                   .ForMember(dest => dest.CountrySights, opt => opt.MapFrom(src => src.Sights));
            CreateMap<Sight, CountrySightResponse>()
                .ForMember(dest => dest.ImageURLs, opt => opt.MapFrom(src => src.SightPhotos.Select(p => p.Url)));

            CreateMap<CreateCountryRequest, Country>();
        }
    }
}
