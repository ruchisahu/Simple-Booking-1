FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /code

COPY . .
RUN dotnet restore

RUN dotnet publish --output /out/ --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
COPY --from=build-env /out /app/
WORKDIR /app
ENTRYPOINT ["dotnet", "EventCatalogAPI.dll"]