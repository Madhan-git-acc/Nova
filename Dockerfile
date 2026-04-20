# Stage 1: Build Stage - Use the .NET 10 SDK image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code and build/publish the application
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime Stage - Use the .NET 10 ASP.NET Core Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Copy the wwwroot folder from the build stage
COPY --from=build /app/wwwroot ./wwwroot

# Configure the container to listen on port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "CLOTHAPI.dll"]