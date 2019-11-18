FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TattleTrail.Test/TattleTrail.Test.csproj", "TattleTrail.Test/"]
COPY ["TattleTrail/TattleTrail.csproj", "TattleTrail/"]
RUN dotnet restore "TattleTrail.Test/TattleTrail.Test.csproj"
COPY . .
WORKDIR "/src/TattleTrail.Test"
RUN dotnet build "TattleTrail.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TattleTrail.Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TattleTrail.Test.dll"]