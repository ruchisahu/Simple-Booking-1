FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ../WebMvcClient/WebMvcClient.csproj ../WebMvcClient/
RUN dotnet restore ../WebMvcClient/WebMvcClient.csproj
COPY . .
WORKDIR /src/../WebMvcClient
RUN dotnet build WebMvcClient.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebMvcClient.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMvcClient.dll"]
