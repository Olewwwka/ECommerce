using AutoMapper;
using CatalogService.Application.DTO;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributes.Commands.Update
{
    public class UpdateProductAttributeCommandHandler : IRequestHandler<UpdateProductAttributeCommand, Guid>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IMapper _mapper;
        public UpdateProductAttributeCommandHandler(IProductAttributeRepository productAttributeRepository, IMapper mapper)
        {
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var existingAttribute = await _productAttributeRepository.GetByIdAsync(request.Id, cancellationToken);

            if(existingAttribute is null)
            {
                throw new NotFoundException($"Attribute with id {request.Id} not found");
            }

            if (existingAttribute.CategoryId != request.CategoryId)
            {
                throw new CategoryMismatchException($"Category {request.CategoryId} not include attribute {request.Id}");
            }

            _mapper.Map(request, existingAttribute);

            await _productAttributeRepository.UpdateAsync(existingAttribute, cancellationToken);

            return request.Id;
        }
    }
}
