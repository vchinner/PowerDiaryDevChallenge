#Base SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app


# Copy csproj and restore as distinct layers
WORKDIR /src
COPY PowerDiaryDevChallenge.sln ./
COPY PowerDiaryDevChallenge/*.csproj ./PowerDiaryDevChallenge/
COPY PowerDiaryDevChallenge.Chat/*.csproj ./PowerDiaryDevChallenge.Chat/
COPY PowerDiaryDevChallenge.Data/*.csproj ./PowerDiaryDevChallenge.Data/
COPY PowerDiaryDevChallenge.IntegrationTests/*.csproj ./PowerDiaryDevChallenge.IntegrationTests/
COPY PowerDiaryDevChallenge.UnitTests/*.csproj ./PowerDiaryDevChallenge.UnitTests/
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR /src/PowerDiaryDevChallenge.Chat
RUN dotnet build -c Release -o /app
WORKDIR /src/PowerDiaryDevChallenge.Data
RUN dotnet build -c Release -o /app

WORKDIR /src/PowerDiaryDevChallenge
RUN dotnet build -c Release -o /app

RUN dotnet publish -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "PowerDiaryDevChallenge.dll"]