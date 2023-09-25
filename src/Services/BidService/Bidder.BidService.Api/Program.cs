using Bidder.Application.Common.Extension;
using Bidder.BidService.Api.Registration;
using Bidder.BidService.Api.Services;
using Bidder.BidService.Infastructure.Context;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddElasticWithSerilog(Assembly.GetExecutingAssembly().GetName().Name, builder.Configuration, builder.Environment.EnvironmentName);
 

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGrpcReflectionService();
}

app.MigrateDatabase<BidDbContext>();
app.UseRouting();
app.UseAuthorization();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    app.MapGrpcService<BidGrpcService>(); 
});

app.UseHttpsRedirection(); 
app.MapControllers();

app.Run();
