using Asp.Versioning;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pmService.Models;
using pmService.Repositories;
using PmServiceNetCode.Authorization;
using PmServiceNetCode.Data;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Middleware;
using PmServiceNetCode.Models;
using PmServiceNetCode.Repositories;
using PmServiceNetCode.Services;
using PmServiceNetCode.Validation;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenTelemetry().UseAzureMonitor();

builder.Services.Configure<OpenTelemetry.Logs.OpenTelemetryLoggerOptions>(options =>
{
    options.IncludeScopes = true;
});

// Add services to the container
builder.Services.AddScoped<ISqlDataAccess, ClassData>();
builder.Services.AddScoped<IProcedureDataAccess, ClassData>();
builder.Services.AddScoped<IDerakhtTajhizatRepository, DerakhtTajhizatRepository>();
builder.Services.AddScoped<IFarayandRepository, FarayandRepository>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, EndpointPermissionAuthorizationHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateFormDtoValidator>();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
// DbContext for real SQL Server
builder.Services.AddDbContext<MaznetModel>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Maznet_Azure")));
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key is not configured.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer is not configured.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("JWT Audience is not configured.");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
                )
        };
    }
    );
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new EndpointPermissionRequirement())
        .Build();
});

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token."
    });
    
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PmService API",
        Version = "v1"
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PmService API",
        Version = "v2"
    });

    options.DocInclusionPredicate((documentName, apiDescription) =>
    {
        return apiDescription.GroupName == documentName;
    });
});

// Add ClassData as scoped (already registered via interfaces, optional)
builder.Services.AddScoped<ClassData>();

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<MaznetModel>();

//    await DbSeeder.SeedAsync(context);
//}
// Development pipeline

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "PmService API v1");
    options.SwaggerEndpoint(
    "/swagger/v2/swagger.json",
    "PmService API v2");
});
if (app.Environment.IsDevelopment())
{
  
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
//app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Needed for WebApplicationFactory in integration tests
public partial class Program { }
