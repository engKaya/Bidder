using Bidder.BidService.Api.Registration.AppConfigRegistrations;
using Bidder.BidService.Api.Registration.InternalServices;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(kestrelOptions =>
{ 
    kestrelOptions.ListenAnyIP(5012, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
    kestrelOptions.ListenAnyIP(5002, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
    }); 
});
builder.Services.StartConfigRegistrationPipeLine(builder.Configuration);
builder.Services.StartInternalServicePipe(builder.Configuration); 



var app = builder.Build();
app.StartInternalServiceApp();