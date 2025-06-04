FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/BasketService/BasketService.API/*.csproj ./src/BasketService/BasketService.API/
COPY src/BasketService/BasketService.Application/*.csproj ./src/BasketService/BasketService.Application/
COPY src/BasketService/BasketService.Infrastructure/*.csproj ./src/BasketService/BasketService.Infrastructure/
COPY src/BasketService/BasketService.Domain/*.csproj ./src/BasketService/BasketService.Domain/

RUN dotnet restore src/BasketService/BasketService.API/BasketService.API.csproj

COPY src/BasketService/. ./src/BasketService/

WORKDIR /app/src/BasketService/BasketService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/BasketService/BasketService.API/out .

ENTRYPOINT ["dotnet", "BasketService.API.dll"]
