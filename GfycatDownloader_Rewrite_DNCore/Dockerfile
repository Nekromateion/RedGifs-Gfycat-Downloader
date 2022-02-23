FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GfycatDownloader_Rewrite_DNCore/GfycatDownloader_Rewrite_DNCore.csproj", "GfycatDownloader_Rewrite_DNCore/"]
RUN dotnet restore "GfycatDownloader_Rewrite_DNCore/GfycatDownloader_Rewrite_DNCore.csproj"
COPY . .
WORKDIR "/src/GfycatDownloader_Rewrite_DNCore"
RUN dotnet build "GfycatDownloader_Rewrite_DNCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GfycatDownloader_Rewrite_DNCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GfycatDownloader_Rewrite_DNCore.dll"]
