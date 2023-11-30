using Bidder.Application.Common.Extension;
using Bidder.BidService.Api.GrpcServices;
using Bidder.BidService.Api.Registration;
using Bidder.BidService.Infastructure.Context;
using Bidder.Infastructure.Common.Extensions;
using Steeltoe.Discovery.Client;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Bidder.BidService.Api.Registration.AppConfigRegistrations
{
    public static class StartConfigPipe
    {
        public static void StartConfigRegistrationPipeLine(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigRegistrations(configuration);
            services.ConfigureGrpc(configuration);
            services.ConfigureElasticAndConsul(configuration);
        }

        private static void AddConfigRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.ConfigureAuth(configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("*")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
        }

        private static void ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddGrpcReflection();
        }

        private static void ConfigureElasticAndConsul(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddConsulConfig(configuration);
            var enviroment = services.BuildServiceProvider().GetRequiredService<IWebHostEnvironment>();
            services.AddElasticWithSerilog(Assembly.GetExecutingAssembly().GetName().Name, configuration, enviroment.EnvironmentName);
        }
    }
}
