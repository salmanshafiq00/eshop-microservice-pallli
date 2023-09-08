using Discount.gRPC.Data;
using Discount.gRPC.Interfaces;
using Discount.gRPC.Repository;
using Discount.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
services.AddScoped<DapperContext>();
services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddGrpc();

services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();