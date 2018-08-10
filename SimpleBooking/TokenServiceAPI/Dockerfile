FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 60469
EXPOSE 44342

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TokenServiceAPI/TokenServiceAPI.csproj TokenServiceAPI/
RUN dotnet restore TokenServiceAPI/TokenServiceAPI.csproj
COPY . .
WORKDIR /src/TokenServiceAPI
RUN dotnet build TokenServiceAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TokenServiceAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TokenServiceAPI.dll"]
