using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security;
using Persistence;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);

builder.Services.AddSecurityServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwaggerGen();
builder.Services.AddJwtBearerAuthentication(configuration);
builder.Services.AddCustomHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();