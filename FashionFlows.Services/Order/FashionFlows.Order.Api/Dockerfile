#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionFlows.Services/Order/FashionFlows.Order.Api/FashionFlows.Order.Api.csproj", "FashionFlows.Services/Order/FashionFlows.Order.Api/"]
COPY ["FashionFlows.Services/Order/FashionFlow.Order.Application/FashionFlows.Order.Application.csproj", "FashionFlows.Services/Order/FashionFlow.Order.Application/"]
COPY ["FashionFlows.Services/Order/FashionFlows.Order.Domain/FashionFlows.Order.Domain.csproj", "FashionFlows.Services/Order/FashionFlows.Order.Domain/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/FashionFlows.BuildingBlock.Infrastructure.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/FashionFlows.BuildingBlock.Application.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/FashionFlows.BuildingBlock.Domain.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/"]
COPY ["FashionFlows.Services/Order/FashionFlows.Order.Infrastructure/FashionFlows.Order.Infrastructure.csproj", "FashionFlows.Services/Order/FashionFlows.Order.Infrastructure/"]
RUN dotnet restore "./FashionFlows.Services/Order/FashionFlows.Order.Api/FashionFlows.Order.Api.csproj"
COPY . .
WORKDIR "/src/FashionFlows.Services/Order/FashionFlows.Order.Api"
RUN dotnet build "./FashionFlows.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionFlows.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFlows.Order.Api.dll"]