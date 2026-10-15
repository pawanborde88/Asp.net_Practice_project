# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["EmplyeeeManagement.csproj", "./"]
RUN dotnet restore "EmplyeeeManagement.csproj"

# Copy remaining source code and publish
COPY . .
RUN dotnet publish "EmplyeeeManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EmplyeeeManagement.dll"]
