FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY TattleTrail.csproj .
RUN dotnet restore "TattleTrail.csproj"
COPY . .
RUN dotnet build "TattleTrail.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TattleTrail.csproj" -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
#EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TattleTrail.dll"]