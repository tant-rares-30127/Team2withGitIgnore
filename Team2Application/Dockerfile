FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Team2Application.csproj", "./"]
RUN dotnet restore Team2Application.csproj

COPY . .
RUN dotnet build Team2Application.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Team2Application.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Team2Application.dll"]