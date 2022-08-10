using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Infrastructure.Data;
using e_commerce.Helpers;
using e_commerce.Middleware;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Errors;
using e_commerce.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddDbContext<StoreContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();


    
var app = builder.Build(); 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();

        await StoreContextSeed.SeedAsync(context, loggerFactory); ;
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex,"something went wrong seeding");
    }
}

//seeding data
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    StoreContextSeed.Initialize(services);
//}

// Configure the HTTP request pipeline.

app.UseMiddleware<ExeptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
