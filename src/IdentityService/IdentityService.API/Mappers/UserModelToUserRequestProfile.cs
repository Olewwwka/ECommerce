using AutoMapper;
using CatalogService.API.Models;
using IdentityService.BLL.DTO;
using IdentityService.DAL.Entities;

namespace IdentityService.API.Mappers
{
    public class UserModelToUserRequestProfile : Profile
    {
        public UserModelToUserRequestProfile()
        {
            CreateMap<RegisterModel, RegisterRequest>();
        }
    }
}
