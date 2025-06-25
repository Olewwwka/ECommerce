using AutoMapper;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Abstractions.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Enums;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Guid>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileService fileService)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<Guid> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingBrand is null)
            {
                throw new NotFoundException($"Brand with id {request.Id} not found");
            }

            existingBrand.Name = request.Name;

            if (request.Logo is not null)
            {
                await _fileService.DeletePhoto(FileCategory.BrandLogo, null, existingBrand.Name);
                var logoFilePath = await _fileService.SavePhoto(
                  request.Logo,
                  FileCategory.BrandLogo,
                  productName: null,
                  brandName: request.Name);

                existingBrand.LogoUrl = logoFilePath;
            }

            await _brandRepository.UpdateAsync(existingBrand, cancellationToken);

            return existingBrand.Id;
        }
    }
}
