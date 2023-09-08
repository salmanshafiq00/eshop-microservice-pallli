using Catalog.API.Data;
using Catalog.API.Dtos;
using Catalog.API.Interfaces;
using Catalog.API.Seeds;
using Catalog.API.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<MongoSettings>(
    configuration.GetSection(nameof(MongoSettings)));

services.AddSingleton<IMongoSettings>(
    provider => provider.GetRequiredService<IOptions<MongoSettings>>().Value);

services.AddScoped<IProductService, ProductService>(); // service without using context class
//services.AddScoped<IProductService, ProductServiceUsingContext>();      // service using context class

// Testing purpose
services.AddScoped<CatalogMongoDbContext>();

// register hosted service

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedingServices = scope.ServiceProvider;
    var dbContext = seedingServices.GetRequiredService<CatalogMongoDbContext>();
    var seed = new ProductDataSeed(dbContext);
    seed.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();