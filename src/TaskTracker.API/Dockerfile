# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskTracker.API/TaskTracker.API.csproj", "TaskTracker.API/"]
COPY ["TaskTracker.Application/TaskTracker.Application.csproj", "TaskTracker.Application/"]
COPY ["TaskTracker.Domain/TaskTracker.Domain.csproj", "TaskTracker.Domain/"]
COPY ["TaskTracker.Infrastructure/TaskTracker.Infrastructure.csproj", "TaskTracker.Infrastructure/"]

COPY ["Directory.Packages.props", "./"] 

RUN dotnet restore "TaskTracker.API/TaskTracker.API.csproj"

COPY . .
WORKDIR "/src/TaskTracker.API"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TaskTracker.API.dll"]
