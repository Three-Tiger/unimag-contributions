#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniMagContributions/UniMagContributions.csproj", "UniMagContributions/"]
RUN dotnet restore "UniMagContributions/UniMagContributions.csproj"
COPY . .
WORKDIR "/src/UniMagContributions"
RUN dotnet build "UniMagContributions.csproj" -c Release -o /app/build

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef --version 6.0.27

ENV PATH="${PATH}:/root/.dotnet/tools"

# Run EF Core database migrations
RUN dotnet ef database update

FROM build AS publish
RUN dotnet publish "UniMagContributions.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniMagContributions.dll"]