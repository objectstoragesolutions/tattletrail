FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TattleTrail/TattleTrail.csproj", "TattleTrail/"]
RUN dotnet restore "TattleTrail/TattleTrail.csproj"
COPY . .
WORKDIR "/src/TattleTrail"
RUN dotnet build "TattleTrail.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TattleTrail.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TattleTrail.dll"]