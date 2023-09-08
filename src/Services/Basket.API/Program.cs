using Basket.API.gRPCServices;
using Basket.API.Interfaces;
using Basket.API.Services;
using Discount.gRPC.Protos;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("BasketDb");

// Add services to the container.
services.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountGrpcUrl"))
    );
services.AddScoped<DiscountgRpcService>();
services.AddScoped<IBasketRepository, BasketRepository>();

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