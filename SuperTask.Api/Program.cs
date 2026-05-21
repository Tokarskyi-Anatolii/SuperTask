using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SuperTask.Application.Interfaces;
using SuperTask.Infrastructure.RepoInterfaces;
using SuperTaskTracking.Common;
using SuperTaskTracking.Middleware;
using SuperTaskTracking.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddValidatorsFromAssemblyContaining<CreateTaskListRequestDtoValidator>();

builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Services.AddScoped<ITaskListRepository, MongoTaskListRepository>();

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

app.Run();