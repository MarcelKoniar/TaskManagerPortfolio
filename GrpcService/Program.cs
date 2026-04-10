using Application.Extensions;
//using GrpcService.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddGrpc();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//app.Run();

using GrpcService.Services;
using Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
// explicit Kestrel binding for localhost:5001 with HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // ensure HTTPS if client uses https://
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});



// add application & infra services so IToDoTaskService is resolvable
builder.Services.AddApplication();      // ensure Application.DependencyInjection is available
builder.Services.AddInfrastructure();   // ensure repository + db are registered

// gRPC
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ToDoTaskGrpcService>();
app.MapGet("/", () => "gRPC service up");

app.Run();