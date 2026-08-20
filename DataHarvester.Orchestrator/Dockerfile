FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["DataHarvester.Orchestrator/DataHarvester.Orchestrator.csproj", "DataHarvester.Orchestrator/"]
COPY ["DataHarvester.Contracts/DataHarvester.Contracts.csproj", "DataHarvester.Contracts/"]
RUN dotnet restore "DataHarvester.Orchestrator/DataHarvester.Orchestrator.csproj"
COPY . .
WORKDIR "/src/DataHarvester.Orchestrator"
RUN dotnet build "DataHarvester.Orchestrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataHarvester.Orchestrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataHarvester.Orchestrator.dll"]