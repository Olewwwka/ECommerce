using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.BLL.Services;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => new PasswordHasher().HashPassword(src.Password)));
        }
    }
}
