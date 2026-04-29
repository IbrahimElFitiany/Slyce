FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /slyce

COPY preDockerBuild .

RUN dotnet restore "./src/WebAPI"

COPY ./src ./src

RUN dotnet publish "./src/WebAPI" -c Release -o /app/build


FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/build .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]