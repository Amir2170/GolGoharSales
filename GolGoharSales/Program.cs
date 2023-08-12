using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using GolGoharSales.Data;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context

builder.Services.AddDbContext<SalesAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// adding unit of work as a scoped service
builder.Services.AddScoped<UnitOfWork>();

// adding CORS allow origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// configure automapper service
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the controllers.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

// using default and static files

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// enabling CORS
app.UseCors();

app.Run();