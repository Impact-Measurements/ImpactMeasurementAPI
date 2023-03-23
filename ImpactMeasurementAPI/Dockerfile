FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ImpactMeasurementAPI.csproj .
RUN dotnet restore "ImpactMeasurementAPI.csproj"
COPY . .
RUN dotnet publish "ImpactMeasurementAPI.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as final
WORKDIR /app
ENV ASPNETCORE_URLS http://+:44301
COPY --from=build /publish .

EXPOSE 44301

ENTRYPOINT ["dotnet", "ImpactMeasurementAPI.dll","--server.urls", "https://+:44301"]
