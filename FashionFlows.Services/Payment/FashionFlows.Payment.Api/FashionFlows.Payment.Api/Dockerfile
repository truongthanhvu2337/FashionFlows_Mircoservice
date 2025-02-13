#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionFlows.Services/Payment/FashionFlows.Payment.Api/FashionFlows.Payment.Api/FashionFlows.Payment.Api.csproj", "FashionFlows.Services/Payment/FashionFlows.Payment.Api/FashionFlows.Payment.Api/"]
COPY ["FashionFlows.Services/Payment/FashionFlows.Payment.Application/FashionFlows.Payment.Application.csproj", "FashionFlows.Services/Payment/FashionFlows.Payment.Application/"]
COPY ["FashionFlows.Services/Payment/FashionFlows.Payment.Domain/FashionFlows.Payment.Domain.csproj", "FashionFlows.Services/Payment/FashionFlows.Payment.Domain/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/FashionFlows.BuildingBlock.Infrastructure.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/FashionFlows.BuildingBlock.Application.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/FashionFlows.BuildingBlock.Domain.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/"]
COPY ["FashionFlows.Services/Payment/FashionFlows.Payment.Infrastructure/FashionFlows.Payment.Infrastructure.csproj", "FashionFlows.Services/Payment/FashionFlows.Payment.Infrastructure/"]
RUN dotnet restore "./FashionFlows.Services/Payment/FashionFlows.Payment.Api/FashionFlows.Payment.Api/FashionFlows.Payment.Api.csproj"
COPY . .
WORKDIR "/src/FashionFlows.Services/Payment/FashionFlows.Payment.Api/FashionFlows.Payment.Api"
RUN dotnet build "./FashionFlows.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionFlows.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFlows.Payment.Api.dll"]