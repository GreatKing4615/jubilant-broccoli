#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# install psql
RUN apt-get update
RUN apt-get -y install postgresql-client

# make wait-for-postgres.sh executable
RUN chmod +x ./wait-for-postgres.sh
COPY ["JubilantBroccoli/JubilantBroccoli.csproj", "JubilantBroccoli/"]
RUN dotnet restore "JubilantBroccoli/JubilantBroccoli.csproj"
COPY . .
WORKDIR "/src/JubilantBroccoli"
RUN dotnet build "JubilantBroccoli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JubilantBroccoli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JubilantBroccoli.dll"]