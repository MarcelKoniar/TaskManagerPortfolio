using GrpcServiceDemo.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    //options.ListenLocalhost(5001, listenOptions =>
    //{
    //    listenOptions.UseHttps(); // ensure HTTPS if client uses https://
    //    listenOptions.Protocols = HttpProtocols.Http2;
    //});

    options.ListenAnyIP(5001, o =>
    {
        o.UseHttps(); // ensure HTTPS if client uses https://
        o.Protocols = HttpProtocols.Http2;
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
