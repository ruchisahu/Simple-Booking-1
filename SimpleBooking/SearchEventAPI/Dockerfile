FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SearchEventAPI/SearchEventAPI.csproj SearchEventAPI/
RUN dotnet restore SearchEventAPI/SearchEventAPI.csproj
COPY . .
WORKDIR /src/SearchEventAPI
RUN dotnet build SearchEventAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SearchEventAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SearchEventAPI.dll"]
