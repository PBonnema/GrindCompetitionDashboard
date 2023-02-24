# syntax=docker/dockerfile:1.2
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app

###################
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS publish
ARG GrindCompetitionAPIDir
ARG DataAccessDir

# TODO also copy over the .sln because you can do a single dotnet restore on it instead of separate restores
WORKDIR /src/DataAccess
COPY ${DataAccessDir}/*.csproj ./
RUN dotnet restore "DataAccess.csproj"

WORKDIR /src/GrindCompetitionAPI
COPY ${GrindCompetitionAPIDir}/*.csproj ./
RUN dotnet restore "GrindCompetitionAPI.csproj"

COPY ${GrindCompetitionAPIDir}/ /src/GrindCompetitionAPI/
COPY ${DataAccessDir}/ /src/DataAccess/
RUN dotnet publish --no-restore "GrindCompetitionAPI.csproj" -c Release -o /app/publish

###################
FROM base AS final
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "GrindCompetitionAPI.dll"]
