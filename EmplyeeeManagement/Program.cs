using AutoMapper;
using EmployeeManagement.Authorization.Handlers;
using EmployeeManagement.Authorization.Requirements;
using EmployeeManagement.Mappings;
using EmployeeManagement.Middleware;
using EmployeeManagement.Repository.Implementation;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Services.Implementation;
using EmployeeManagement.Services.Interfaces;
using EmplyeeeManagement.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IFileService, FileService>();
// ===========================
// Serilog
// ===========================
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// ===========================
// Controllers
// ===========================
builder.Services.AddControllers();

// ===========================
// CORS
// ===========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ===========================
// Database
// ===========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("EmployeeConnection");
    if (!string.IsNullOrEmpty(connStr) && connStr.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connStr);
    }
    else
    {
        options.UseSqlServer(connStr);
    }
});

// ===========================
// AutoMapper
// ===========================
builder.Services.AddAutoMapper(typeof(EmployeeProfile));

// ===========================
// Dependency Injection
// ===========================
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ===========================
// JWT Authentication
// ===========================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HRDepartment", policy =>
        policy.Requirements.Add(new DepartmentRequirement("HR")));
});
// ===========================
// Swagger
// ===========================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token"
    });

    options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "Could not automatically ensure database creation.");
    }
}

// ===========================
// Logging
// ===========================
Log.Information("Employee Management API Started Successfully.");

// ===========================
// Swagger
// ===========================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API v1");
    c.RoutePrefix = string.Empty; // Serves Swagger UI at application root (e.g. https://your-app/)
});


// ===========================
// Global Exception Middleware
// ===========================
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ===========================
// CORS
// ===========================
app.UseCors("AllowAll");

// ===========================
// HTTPS
// ===========================
app.UseHttpsRedirection();
app.UseStaticFiles();
// ===========================
// Authentication & Authorization
// ===========================
app.UseAuthentication();
app.UseAuthorization();

// ===========================
// Controllers
// ===========================
app.MapControllers();

app.Run();