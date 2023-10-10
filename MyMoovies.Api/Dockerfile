FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyMoovies.Api/MyMooviesApi.csproj", "MyMoovies.Api/"]
RUN dotnet restore "MyMoovies.Api/MyMooviesApi.csproj"
COPY . .
WORKDIR "/src/MyMoovies.Api"
RUN dotnet build "MyMooviesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMooviesApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMooviesApi.dll"]