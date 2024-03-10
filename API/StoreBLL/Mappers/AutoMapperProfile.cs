using AutoMapper;
using Microsoft.Extensions.Logging;
using StoreBLL.DTO;
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
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.MainImgaeURL));

            CreateMap<Country, CountryDetailsDto>()
                   .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.SecondaryImageURL))
                   .ForMember(dest => dest.CountrySightDtos, opt => opt.MapFrom(src => src.Sights));
            CreateMap<Sight, CountrySightDto>()
                .ForMember(dest => dest.ImageURLs, opt => opt.MapFrom(src => src.SightPhotos.Select(p => p.Url)));
        }
    }
}
