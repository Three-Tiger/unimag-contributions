FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniMagContributions/UniMagContributions.csproj", "UniMagContributions/"]
RUN dotnet restore "UniMagContributions/UniMagContributions.csproj"
COPY . .
WORKDIR "/src/UniMagContributions"
RUN dotnet build "UniMagContributions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniMagContributions.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniMagContributions.dll"]