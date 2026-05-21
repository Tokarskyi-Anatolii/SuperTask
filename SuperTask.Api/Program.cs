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

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<UserIdMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();