#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionFlows.Services/Products/FashionFlows.Product.Api/FashionFlows.Product.Api.csproj", "FashionFlows.Services/Products/FashionFlows.Product.Api/"]
COPY ["FashionFlows.Services/Products/FashionFlows.Product.Application/FashionFlows.Product.Application.csproj", "FashionFlows.Services/Products/FashionFlows.Product.Application/"]
COPY ["FashionFlows.Services/Products/FashionFlows.Product.Infrastructure/FashionFlows.Product.Infrastructure.csproj", "FashionFlows.Services/Products/FashionFlows.Product.Infrastructure/"]
RUN dotnet restore "./FashionFlows.Services/Products/FashionFlows.Product.Api/FashionFlows.Product.Api.csproj"
COPY . .
WORKDIR "/src/FashionFlows.Services/Products/FashionFlows.Product.Api"
RUN dotnet build "./FashionFlows.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionFlows.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFlows.Product.Api.dll"]