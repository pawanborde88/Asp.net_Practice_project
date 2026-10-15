# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["EmplyeeeManagement/EmplyeeeManagement.csproj", "EmplyeeeManagement/"]
RUN dotnet restore "EmplyeeeManagement/EmplyeeeManagement.csproj"

# Copy remaining source code and publish
COPY . .
WORKDIR "/src/EmplyeeeManagement"
RUN dotnet publish "EmplyeeeManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
COPY EmplyeeeManagement/employee_management.db .

ENTRYPOINT ["dotnet", "EmplyeeeManagement.dll"]
