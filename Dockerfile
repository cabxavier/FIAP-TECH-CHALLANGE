FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./ ./

RUN dotnet restore

RUN dotnet build --no-restore -c Release

RUN dotnet publish -c Release -o out

FROM build AS test
WORKDIR /app
RUN dotnet test --no-build -c Release --verbosity normal

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS root

COPY --from=build /app/out .
EXPOSE 80

ENTRYPOINT ["dotnet", "TechChallange.Api.dll"]