using Bidder.Application.Common.Extension;
using Bidder.BidService.Api.Registration;
using Bidder.BidService.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(); 
builder.Services.AddElasticWithSerilog(Assembly.GetExecutingAssembly().GetName().Name, builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddGrpcReflection();
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5012, o => o.Protocols =
        HttpProtocols.Http2);
    options.ListenLocalhost(6012, o => o.Protocols =
    HttpProtocols.Http2);
});

var app = builder.Build(); 
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
 
// Configure the HTTP request pipeline.
app.MapGrpcService<BidGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
