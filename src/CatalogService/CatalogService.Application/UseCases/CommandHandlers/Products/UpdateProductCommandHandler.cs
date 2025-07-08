using AutoMapper;
using CatalogService.Application.DTO.Product;
using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.Products
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingProduct is null)
            {
                throw new NotFoundException($"Product with id {request.Id} not found");
            }

            _mapper.Map(request.product, existingProduct);

            await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            var updatedProduct = _mapper.Map<ProductDto>(existingProduct);

            return updatedProduct;
        }
    }
}
