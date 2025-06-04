FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/IdentityService/IdentityService.API/*.csproj ./src/IdentityService/IdentityService.API/
COPY src/IdentityService/IdentityService.BLL/*.csproj ./src/IdentityService/IdentityService.BLL/
COPY src/IdentityService/IdentityService.DAL/*.csproj ./src/IdentityService/IdentityService.DAL/

RUN dotnet restore src/IdentityService/IdentityService.API/IdentityService.API.csproj

COPY src/IdentityService/. ./src/IdentityService/

WORKDIR /app/src/IdentityService/IdentityService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/IdentityService/IdentityService.API/out .

ENTRYPOINT ["dotnet", "IdentityService.API.dll"]
