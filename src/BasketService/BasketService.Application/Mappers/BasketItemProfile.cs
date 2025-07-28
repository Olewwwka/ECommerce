using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Domain.Entities;

namespace BasketService.Application.Mappers
{
    public class BasketItemProfile : Profile
    {
        public BasketItemProfile()
        {
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
