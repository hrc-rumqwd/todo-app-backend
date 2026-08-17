# Dockerfile
ARG DOTNET_VERSION=10.0
ARG BUILD_CONFIGURATION=Release

# --- Stage 1: Build & Publish ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

# Copy source code
WORKDIR /src
COPY ["src/TodoApp.Api/", "TodoApp.Api/"]
COPY ["src/TodoApp.Application/", "TodoApp.Application/"]
COPY ["src/TodoApp.Domain/", "TodoApp.Domain/"]
COPY ["src/TodoApp.Infrastructure/", "TodoApp.Infrastructure/"]
COPY ["src/TodoApp.Shared/", "TodoApp.Shared/"]

# Restore dependencies and build
WORKDIR /src/TodoApp.Api
RUN dotnet restore "./TodoApp.Api.csproj"
RUN dotnet build "./TodoApp.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build 

# Publish
RUN dotnet publish "./TodoApp.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish

# --- Stage 2: Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "TodoApp.Api.dll" ]