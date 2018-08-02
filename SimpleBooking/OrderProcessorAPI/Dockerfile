FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY OrderProcessorAPI/OrderProcessorAPI.csproj OrderProcessorAPI/
RUN dotnet restore OrderProcessorAPI/OrderProcessorAPI.csproj
COPY . .
WORKDIR /src/OrderProcessorAPI
RUN dotnet build OrderProcessorAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OrderProcessorAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderProcessorAPI.dll"]
