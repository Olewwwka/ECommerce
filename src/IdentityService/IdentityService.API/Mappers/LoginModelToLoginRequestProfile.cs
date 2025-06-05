using AutoMapper;
using CatalogService.API.Models;
using IdentityService.BLL.DTO;

namespace IdentityService.API.Mappers
{
    public class LoginModelToLoginRequestProfile : Profile
    {
        public LoginModelToLoginRequestProfile()
        {
            CreateMap<LoginModel, LoginRequest>();
        }
    }
}
