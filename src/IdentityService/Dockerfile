FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY IdentityService.API/IdentityService.API.csproj ./src/IdentityService.API/
COPY IdentityService.BLL/IdentityService.BLL.csproj ./src/IdentityService.BLL/
COPY IdentityService.DAL/IdentityService.DAL.csproj ./src/IdentityService.DAL/

RUN dotnet restore src/IdentityService.API/IdentityService.API.csproj

COPY . ./src/

WORKDIR /app/src/IdentityService.API

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/src/IdentityService.API/out .

ENTRYPOINT ["dotnet", "IdentityService.API.dll"]
