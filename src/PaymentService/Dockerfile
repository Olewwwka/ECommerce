FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY PaymentService.API/PaymentService.API.csproj ./src/PaymentService.API/
COPY PaymentService.Application/PaymentService.Application.csproj ./src/PaymentService.Application/
COPY PaymentService.Infrastructure/PaymentService.Infrastructure.csproj ./src/PaymentService.Infrastructure/
COPY PaymentService.Domain/PaymentService.Domain.csproj ./src/PaymentService.Domain/
COPY PaymentService.Persistence/PaymentService.Persistence.csproj ./src/PaymentService.Persistence/

RUN dotnet restore src/PaymentService.API/PaymentService.API.csproj

COPY . ./src/

WORKDIR /app/src/PaymentService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/PaymentService.API/out .

ENTRYPOINT ["dotnet", "PaymentService.API.dll"]
