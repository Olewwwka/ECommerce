using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingCategory is null)
            {
                throw new NotFoundException($"Category with id {request.Id} not found");
            }

            await _categoryRepository.DeleteAsync(existingCategory, cancellationToken);

            return true;
        }
    }
}
