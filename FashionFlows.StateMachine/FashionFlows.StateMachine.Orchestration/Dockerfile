#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionFlows.StateMachine/FashionFlows.StateMachine.Orchestration/FashionFlows.StateMachine.Orchestration.csproj", "FashionFlows.StateMachine/FashionFlows.StateMachine.Orchestration/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/FashionFlows.BuildingBlock.Infrastructure.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Infrastructure/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/FashionFlows.BuildingBlock.Application.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Application/"]
COPY ["FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/FashionFlows.BuildingBlock.Domain.csproj", "FashionFlows.BuildingBlock/FashionFlows.BuildingBlock.Shared/"]
RUN dotnet restore "./FashionFlows.StateMachine/FashionFlows.StateMachine.Orchestration/FashionFlows.StateMachine.Orchestration.csproj"
COPY . .
WORKDIR "/src/FashionFlows.StateMachine/FashionFlows.StateMachine.Orchestration"
RUN dotnet build "./FashionFlows.StateMachine.Orchestration.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionFlows.StateMachine.Orchestration.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFlows.StateMachine.Orchestration.dll"]