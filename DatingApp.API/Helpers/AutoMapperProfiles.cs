using System.Linq;
using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UsersForListDTO>()
              .ForMember(dest => dest.PhotoUrl, 
                         options => options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
              .ForMember(dest => dest.Age,
                         options => options.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailDTO>()
              .ForMember(dest => dest.PhotoUrl, 
                         options => options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
              .ForMember(dest => dest.Age,
                         options => options.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoForDetailDTO>();
        }
    }
}