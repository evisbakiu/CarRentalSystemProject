# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy everything into the container
COPY . ./

# Restore dependencies and publish
RUN dotnet restore CarRentalSystem.csproj
RUN dotnet publish CarRentalSystem.csproj -c Release -o /out

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy the published output from build stage
COPY --from=build /out .

# Set the entry point to the built DLL
ENTRYPOINT ["dotnet", "CarRentalSystem.dll"]
