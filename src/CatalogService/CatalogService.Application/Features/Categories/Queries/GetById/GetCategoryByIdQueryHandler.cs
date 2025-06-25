using AutoMapper;
using CatalogService.Application.DTO;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryWithAttributesDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryWithAttributesDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.id, cancellationToken);

            if (existingCategory is null)
            {
                throw new NotFoundException($"Category with id {request.id} not found.");
            }

            var category = _mapper.Map<CategoryWithAttributesDto>(existingCategory);

            return category;
        }
    }
}
