FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish ConsoleApp/ConsoleApp.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Copy the SQLite database file
COPY ConsoleApp/Data/StackTrack.db ./StackTrack.db
ENTRYPOINT [ "dotnet", "ConsoleApp.dll" ]