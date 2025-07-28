using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Domain.Entities;

namespace BasketService.Application.Mappers
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>()
                .ReverseMap();
        }
    }
}
