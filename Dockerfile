FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY EfCoreRelationshipDemo.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "EfCoreRelationshipDemo.dll"]
