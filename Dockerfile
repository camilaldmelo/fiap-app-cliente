# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory.
WORKDIR /.

# Copy the entire repository into the container.
COPY . .

# Restore the dependencies and tools of the project.
RUN dotnet restore "nuget-fiap-app-produto-server/nuget-fiap-app-produto-server.csproj"

# Build the project.
RUN dotnet build "nuget-fiap-app-produto-server/nuget-fiap-app-produto-server.csproj" -c Release -o /app/build

# Publish the application.
FROM build AS publish
RUN dotnet publish "nuget-fiap-app-produto-server/nuget-fiap-app-produto-server.csproj" -c Release -o /app/publish

# Use the official ASP.NET core runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nuget-fiap-app-produto-server.dll"]
