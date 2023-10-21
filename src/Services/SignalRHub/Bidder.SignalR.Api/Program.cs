using Bidder.Application.Common.Extension;
using Bidder.SignalR.Api.Extensions;
using Bidder.SignalR.Application.AuctionHub;
using Microsoft.AspNetCore.Http.Connections;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddElasticWithSerilog(Assembly.GetExecutingAssembly().GetName().Name, builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddHealthChecks();
var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<BidHub>("hub/bidhub", options =>
{
     
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling; 
});
app.UseConsul();
app.UseCors();
app.Run();
