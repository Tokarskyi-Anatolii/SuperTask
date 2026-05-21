using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SuperTask.Application.Interfaces;
using SuperTask.Application.Services;
using SuperTask.Infrastructure.RepoInterfaces;
using SuperTaskTracking.Common;
using SuperTaskTracking.Helpers.Seeding;
using SuperTaskTracking.Middleware;
using SuperTaskTracking.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("UserHeaderAuth", new OpenApiSecurityScheme
    {
        Name = "X-User-Id",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Please enter a valid User Guid to test the endpoints."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UserHeaderAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddValidatorsFromAssemblyContaining<CreateTaskListRequestDtoValidator>();

builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Services.AddScoped<ITaskListRepository, MongoTaskListRepository>();
builder.Services.AddScoped<ITaskListService, TaskListService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    var connectionString = !string.IsNullOrEmpty(settings.ConnectionString) 
        ? settings.ConnectionString 
        : builder.Configuration["MongoDb__ConnectionString"];
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    
    var databaseName = !string.IsNullOrEmpty(settings.Database) 
        ? settings.Database 
        : builder.Configuration["MongoDb__Database"] ?? "SuperTaskDb";

    return client.GetDatabase(databaseName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<UserIdMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    await MongoDataSeeder.SeedDatabaseAsync(database);
}

app.Run();