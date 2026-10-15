# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy all project files
COPY . .
WORKDIR "/src/Just Practices .net"

# Restore and Publish
RUN dotnet restore "Just Practices .net.csproj"
RUN dotnet publish "Just Practices .net.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Just Practices .net.dll"]

