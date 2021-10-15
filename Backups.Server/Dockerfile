FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Backups.Server/Backups.Server.csproj", "Backups.Server/"]
RUN dotnet restore "Backups.Server/Backups.Server.csproj"
COPY . .
WORKDIR "/src/Backups.Server"
RUN dotnet build "Backups.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backups.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backups.Server.dll"]
