#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PlayerPerformanceService.csproj", "./"]
RUN dotnet restore "PlayerPerformanceService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "PlayerPerformanceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayerPerformanceService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080/

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayerPerformanceService.dll"]