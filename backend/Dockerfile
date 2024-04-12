# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Create a new .NET Web API project
RUN dotnet new webapi -n Slamdunk.WebApi

# Change to the newly created project directory
WORKDIR /app/Slamdunk.WebApi

# Install Swashbuckle EntityFrameworkCore PostgreSQL package
RUN dotnet add package Microsoft.EntityFrameworkCore.Design
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
RUN dotnet add package Swashbuckle.AspNetCore
RUN dotnet add package itext7.pdfhtml
RUN dotnet add package itext7.bouncy-castle-adapter

# Build the application
RUN dotnet build

# Expose the necessary port (e.g., 80 for HTTP)
EXPOSE 5214

# Start the app
ENTRYPOINT ["dotnet", "run", "--urls", "http://0.0.0.0:5214"]
