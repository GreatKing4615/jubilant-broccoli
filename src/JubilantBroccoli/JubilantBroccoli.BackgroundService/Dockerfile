#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JubilantBroccoli.BackgroundService/JubilantBroccoli.BackgroundService.csproj", "JubilantBroccoli.BackgroundService/"]
COPY ["JubilantBroccoli.BusinessLogic.Contracts/JubilantBroccoli.BusinessLogic.Contracts.csproj", "JubilantBroccoli.BusinessLogic.Contracts/"]
COPY ["JubilantBroccoli.Domain.Models/JubilantBroccoli.Domain.Models.csproj", "JubilantBroccoli.Domain.Models/"]
COPY ["JubilantBroccoli.Domain.Core/JubilantBroccoli.Domain.Core.csproj", "JubilantBroccoli.Domain.Core/"]
COPY ["JubilantBroccoli.Domain.Dtos/JubilantBroccoli.Domain.Dtos.csproj", "JubilantBroccoli.Domain.Dtos/"]
COPY ["JubilantBroccoli.Infrastructure.UnitOfWork/JubilantBroccoli.Infrastructure.UnitOfWork.csproj", "JubilantBroccoli.Infrastructure.UnitOfWork/"]
COPY ["JubilantBroccoli.Infrastructure.Core/JubilantBroccoli.Infrastructure.Core.csproj", "JubilantBroccoli.Infrastructure.Core/"]
RUN dotnet restore "JubilantBroccoli.BackgroundService/JubilantBroccoli.BackgroundService.csproj"
COPY . .
WORKDIR "/src/JubilantBroccoli.BackgroundService"
RUN dotnet build "JubilantBroccoli.BackgroundService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JubilantBroccoli.BackgroundService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JubilantBroccoli.BackgroundService.dll"]