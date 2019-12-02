FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /webapi
COPY ["docker-core-couchbase/docker-core-couchbase.csproj", "docker-core-couchbase/"]
RUN dotnet restore "docker-core-couchbase/docker-core-couchbase.csproj"
COPY . .
WORKDIR /webapi/docker-core-couchbase
RUN dotnet build "docker-core-couchbase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "docker-core-couchbase.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "docker-core-couchbase.dll"]