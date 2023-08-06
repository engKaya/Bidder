using Bidder.IdentityService.Api.Extensions;
using Bidder.IdentityService.Api.Registration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidatorFilterAttr>(); 
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}); ;
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.ConfigureValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseAuthorization();

app.MapControllers();

app.Run();
