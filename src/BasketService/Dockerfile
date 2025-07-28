FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY BasketService.API/*.csproj ./BasketService.API/
COPY BasketService.Application/*.csproj ./BasketService.Application/
COPY BasketService.Infrastructure/*.csproj ./BasketService.Infrastructure/
COPY BasketService.Domain/*.csproj ./BasketService.Domain/

RUN dotnet restore BasketService.API/BasketService.API.csproj

COPY . .

WORKDIR /app/BasketService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/BasketService.API/out .

ENTRYPOINT ["dotnet", "BasketService.API.dll"]
