using AutoMapper;
using OrderService.Application.DTO;
using OrderService.Domain.Entities;

namespace OrderService.Application.Mappers
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Count));

            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
