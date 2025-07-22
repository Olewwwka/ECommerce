using AutoMapper;
using CatalogService.Application.UseCases.Commands.Categories;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category, cancellationToken);

            return category.Name;
        }
    }
}
