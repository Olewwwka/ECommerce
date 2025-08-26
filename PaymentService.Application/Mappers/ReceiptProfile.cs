using AutoMapper;
using PaymentService.Application.DTO;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.Mappers
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<PaymentRequest, Receipt>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<ReceiptDTO, Receipt>();

            CreateMap<Receipt, ReceiptDTO>();

            CreateMap<PutUpUserBallanceDTO, Receipt>()
             .ForMember(dest => dest.OrderId, opt => opt.Ignore())
             .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Amount))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PaymentStatus.Success))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        }
    }
}
