FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["QuickPostApi/QuickPostApi.csproj", "QuickPostApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
RUN dotnet restore "QuickPostApi/QuickPostApi.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/QuickPostApi"

# Build the application
RUN dotnet build "QuickPostApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "QuickPostApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "QuickPostApi.dll"]
