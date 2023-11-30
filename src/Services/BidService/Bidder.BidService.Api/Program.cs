using Bidder.BidService.Api.Registration.AppConfigRegistrations;
using Bidder.BidService.Api.Registration.InternalServices;


var builder = WebApplication.CreateBuilder(args);
builder.Services.StartConfigRegistrationPipeLine(builder.Configuration);
builder.Services.StartInternalServicePipe(builder.Configuration); 

var app = builder.Build();
app.StartInternalServiceApp();