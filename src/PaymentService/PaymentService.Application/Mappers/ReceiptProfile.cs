using AutoMapper;
using PaymentService.Application.DTO;
using PaymentService.Domain.Entities;

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
        }
    }
}
