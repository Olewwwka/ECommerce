﻿using AutoMapper;
using CatalogService.Application.DTO.Categories;
using CatalogService.Application.UseCases.Queries.Categories;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.QueryHandlers.Categories
{
    public class GetPagedCategoriesQueryHandler : IRequestHandler<GetPagedCategoriesQuery, PagedItems<CategoryWithAttributesDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetPagedCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<PagedItems<CategoryWithAttributesDto>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            var items = _mapper.Map<PagedItems<CategoryWithAttributesDto>>(categories);

            return items;
        }
    }
}
