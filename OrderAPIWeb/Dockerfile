FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ../OrderAPIWeb/OrderAPIWeb.csproj ../OrderAPIWeb/
RUN dotnet restore ../OrderAPIWeb/OrderAPIWeb.csproj
COPY . .
WORKDIR /src/../OrderAPIWeb
RUN dotnet build OrderAPIWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OrderAPIWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderAPIWeb.dll"]
