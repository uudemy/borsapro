#!/bin/bash
# Run this script to generate the .sln file and link the existing projects

dotnet new sln -n TradingApp
dotnet sln add src/TradingApp.Domain/TradingApp.Domain.csproj
dotnet sln add src/TradingApp.Application/TradingApp.Application.csproj
dotnet sln add src/TradingApp.Infrastructure/TradingApp.Infrastructure.csproj
dotnet sln add src/TradingApp.Api/TradingApp.Api.csproj
dotnet sln add tests/TradingApp.ApiTests/TradingApp.ApiTests.csproj

echo "Solution created and projects linked successfully!"
echo "Run 'docker-compose up -d' to start the database."
echo "Run 'dotnet run --project src/TradingApp.Api' to start the backend."
