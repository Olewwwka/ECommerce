FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/PaymentService/PaymentService.API/*.csproj ./src/PaymentService/PaymentService.API/
COPY src/PaymentService/PaymentService.Application/*.csproj ./src/PaymentService/PaymentService.Application/
COPY src/PaymentService/PaymentService.Infrastructure/*.csproj ./src/PaymentService/PaymentService.Infrastructure/
COPY src/PaymentService/PaymentService.Domain/*.csproj ./src/PaymentService/PaymentService.Domain/
COPY src/PaymentService/PaymentService.Persistence/*.csproj ./src/PaymentService/PaymentService.Persistence/

RUN dotnet restore src/PaymentService/PaymentService.API/PaymentService.API.csproj

COPY src/PaymentService/. ./src/PaymentService/

WORKDIR /app/src/PaymentService/PaymentService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/PaymentService/PaymentService.API/out .

ENTRYPOINT ["dotnet", "PaymentService.API.dll"]
