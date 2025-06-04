FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/OrderService/OrderService.API/*.csproj ./src/OrderService/OrderService.API/
COPY src/OrderService/OrderService.Application/*.csproj ./src/OrderService/OrderService.Application/
COPY src/OrderService/OrderService.Infrastructure/*.csproj ./src/OrderService/OrderService.Infrastructure/
COPY src/OrderService/OrderService.Domain/*.csproj ./src/OrderService/OrderService.Domain/
COPY src/OrderService/OrderService.Persistence/*.csproj ./src/OrderService/OrderService.Persistence/

RUN dotnet restore src/OrderService/OrderService.API/OrderService.API.csproj

COPY src/OrderService/. ./src/OrderService/

WORKDIR /app/src/OrderService/OrderService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/OrderService/OrderService.API/out .

ENTRYPOINT ["dotnet", "OrderService.API.dll"]
