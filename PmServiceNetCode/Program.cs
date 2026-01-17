using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<ISqlDataAccess, ClassData>();
builder.Services.AddScoped<IProcedureDataAccess, ClassData>();
builder.Services.AddScoped<IFarayandRepository, FarayandRepository>();

// DbContext for real SQL Server
builder.Services.AddDbContext<MaznetModel>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Maznet_Azure")));

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add ClassData as scoped (already registered via interfaces, optional)
builder.Services.AddScoped<ClassData>();

var app = builder.Build();

// Development pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Needed for WebApplicationFactory in integration tests
public partial class Program { }
