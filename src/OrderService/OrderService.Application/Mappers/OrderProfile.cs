using AutoMapper;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => OrderStatus.Created))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.MapFrom(x => x.Request.Items));

            CreateMap<Order, OrderDto>();
            
        }
    }
}
