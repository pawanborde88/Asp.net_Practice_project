using EMPManagement.Data;
using Just_Practices_.net.Repository_Pattern;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Just_Practices_.net.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<EMPManagementDBContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("NZWalksConnectionString"));
});

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(AutomapProfile).Assembly);// Dependency Injection
builder.Services.AddScoped<IAllEmployess, EmployeeRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Build app
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        // c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowSwagger");

app.UseAuthorization();

app.MapControllers();

app.Run();