FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /code

COPY . .
RUN dotnet restore

RUN dotnet publish --output /out/ --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
COPY --from=build /out /app/
WORKDIR /app
ENTRYPOINT ["dotnet", "OrderProcessorAPI.dll"]