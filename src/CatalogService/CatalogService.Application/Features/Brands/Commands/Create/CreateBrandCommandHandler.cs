using AutoMapper;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Abstractions.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, string>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileService fileService)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<string> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request);

            if (request.Logo is not null)
            {
                var logoFilePath = await _fileService.SavePhoto(
                    request.Logo,
                    FileCategory.BrandLogo,
                    productName: null,
                    brandName: request.Name);

                brand.LogoUrl = logoFilePath;
            }

            await _brandRepository.AddAsync(brand, cancellationToken);

            return brand.Name;
        }
    }
}
