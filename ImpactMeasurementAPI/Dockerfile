FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ImpactMeasurementAPI.csproj .
RUN dotnet restore "ImpactMeasurementAPI.csproj"
COPY . .
RUN dotnet publish "ImpactMeasurementAPI.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "ImpactMeasurementAPI.dll"]
