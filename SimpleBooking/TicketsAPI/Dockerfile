FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY TicketsAPI/TicketsAPI.csproj TicketsAPI/
RUN dotnet restore TicketsAPI/TicketsAPI.csproj
COPY . .
WORKDIR /src/TicketsAPI
RUN dotnet build TicketsAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TicketsAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TicketsAPI.dll"]
