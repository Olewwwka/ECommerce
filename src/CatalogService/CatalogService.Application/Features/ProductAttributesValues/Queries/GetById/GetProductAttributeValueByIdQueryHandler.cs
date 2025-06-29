using AutoMapper;
using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Specifications.Infrastructure;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Queries.GetById
{
    public class GetProductAttributeValueByIdQueryHandler : IRequestHandler<GetProductAttributeValueByIdQuery, ProductAttributeValueDto>
    {
        private readonly IProductAttributeValueRepository _productAttributeValueRepository;
        private readonly IMapper _mapper;

        public GetProductAttributeValueByIdQueryHandler(IProductAttributeValueRepository productAttributeValueRepository, IMapper mapper)
        {
            _productAttributeValueRepository = productAttributeValueRepository;
            _mapper = mapper;
        }

        public async Task<ProductAttributeValueDto> Handle(GetProductAttributeValueByIdQuery request, CancellationToken cancellationToken)
        {
            var valueSpec = new AttributeValueByProductIdSpecification(request.ProductId, request.ProductAttributeId);

            var valueEntity = await _productAttributeValueRepository.GetOneBySpecAsync(valueSpec, cancellationToken);

            if(valueEntity is null)
            {
                throw new NotFoundException($"Product with id {request.ProductId} does't have value to attribute {request.ProductAttributeId}");
            }

            var value = _mapper.Map<ProductAttributeValueDto>(valueEntity);

            return value;
        }
    }
}
