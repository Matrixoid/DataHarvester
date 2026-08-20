FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src

COPY ["DataHarvester.Worker/DataHarvester.Worker.csproj", "DataHarvester.Worker/"]
COPY ["DataHarvester.Contracts/DataHarvester.Contracts.csproj", "DataHarvester.Contracts/"]
RUN dotnet restore "DataHarvester.Worker/DataHarvester.Worker.csproj"

COPY . .
WORKDIR "/src/DataHarvester.Worker"
RUN dotnet publish "DataHarvester.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR /app/publish
RUN pwsh playwright.ps1 install --with-deps chromium
ENTRYPOINT ["dotnet", "DataHarvester.Worker.dll"]