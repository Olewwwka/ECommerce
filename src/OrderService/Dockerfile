FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY OrderService.API/OrderService.API.csproj ./src/OrderService.API/
COPY OrderService.Application/OrderService.Application.csproj ./src/OrderService.Application/
COPY OrderService.Infrastructure/OrderService.Infrastructure.csproj ./src/OrderService.Infrastructure/
COPY OrderService.Domain/OrderService.Domain.csproj ./src/OrderService.Domain/
COPY OrderService.Persistence/OrderService.Persistence.csproj ./src/OrderService.Persistence/

RUN dotnet restore src/OrderService.API/OrderService.API.csproj

COPY . ./src/

WORKDIR /app/src/OrderService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/OrderService.API/out .

ENTRYPOINT ["dotnet", "OrderService.API.dll"]
