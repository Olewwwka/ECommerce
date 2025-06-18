FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY CatalogService.API/CatalogService.API.csproj ./src/CatalogService.API/
COPY CatalogService.Application/CatalogService.Application.csproj ./src/CatalogService.Application/
COPY CatalogService.Infrastructure/CatalogService.Infrastructure.csproj ./src/CatalogService.Infrastructure/
COPY CatalogService.Domain/CatalogService.Domain.csproj ./src/CatalogService.Domain/

RUN dotnet restore src/CatalogService.API/CatalogService.API.csproj

COPY . ./src/

WORKDIR /app/src/CatalogService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/CatalogService.API/out .

ENTRYPOINT ["dotnet", "CatalogService.API.dll"]
