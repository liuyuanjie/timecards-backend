FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Timecards/Timecards.csproj", "Timecards/"]
RUN dotnet restore "Timecards/Timecards.csproj"
COPY . .
WORKDIR "/src/Timecards"
RUN dotnet build "Timecards.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timecards.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timecards.dll"]
