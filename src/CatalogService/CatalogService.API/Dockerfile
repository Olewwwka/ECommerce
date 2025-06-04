FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/CatalogService/CatalogService.API/*.csproj ./src/CatalogService/CatalogService.API/
COPY src/CatalogService/CatalogService.Application/*.csproj ./src/CatalogService/CatalogService.Application/
COPY src/CatalogService/CatalogService.Infrastructure/*.csproj ./src/CatalogService/CatalogService.Infrastructure/
COPY src/CatalogService/CatalogService.Domain/*.csproj ./src/CatalogService/CatalogService.Domain/

RUN dotnet restore src/CatalogService/CatalogService.API/CatalogService.API.csproj

COPY src/CatalogService/. ./src/CatalogService/

WORKDIR /app/src/CatalogService/CatalogService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/CatalogService/CatalogService.API/out .

ENTRYPOINT ["dotnet", "CatalogService.API.dll"]
