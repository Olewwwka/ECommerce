using AutoMapper;
using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(ICategoryRepository categoryRepository, 
            IProductRepository productRepository, 
            IBrandRepository brandRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if(existingCategory is null)
            {
                throw new NotFoundException($"Category with id {request.CategoryId} not found");
            }

            var existingBrand = await _brandRepository.GetByIdAsync(request.BrandId, cancellationToken);

            if (existingBrand is null)
            {
                throw new NotFoundException($"Brand with id {request.BrandId} not found");
            }

            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product, cancellationToken);

            return product.Name;
        }
    }
}
