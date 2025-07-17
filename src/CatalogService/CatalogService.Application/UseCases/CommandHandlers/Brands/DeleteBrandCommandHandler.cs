using CatalogService.Application.UseCases.Commands.Brands;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Abstractions.Services;
using CatalogService.Domain.Enums;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.Brands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IFileService fileService)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
        }
        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

            if(existingBrand is null)
            {
                throw new NotFoundException($"Brand with id {request.Id} not found");
            }

            await _fileService.DeletePhoto(FileCategory.BrandLogo, null, existingBrand.Name);

            await _brandRepository.DeleteAsync(existingBrand, cancellationToken);

            return true;
        }
    }
}
