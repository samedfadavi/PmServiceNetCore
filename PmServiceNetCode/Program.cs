
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISqlDataAccess, ClassData>();
builder.Services.AddScoped<IProcedureDataAccess, ClassData>();
builder.Services.AddScoped<IFarayandRepository, FarayandRepository>();
builder.Services.AddDbContext<MaznetModel>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Maznet_Azure")));
builder.Services.AddControllers();
builder.Services.AddScoped<ClassData>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }