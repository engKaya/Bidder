using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Interfaces;
using Bidder.BidService.Api.GrpcServices;
using Bidder.BidService.Api.Registration.EventBusRegistration;
using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Application.Mapping;
using Bidder.BidService.Infastructure.Context;
using Bidder.BidService.Infastructure.Repos;
using Bidder.BidService.Infastructure.Services;
using Bidder.BidService.Infastructure.Uof;
using Bidder.Domain.Common.Interfaces;
using Bidder.Infastructure.Common.Services;
using System.Reflection;



namespace Bidder.BidService.Api.Registration.InternalServices
{
    public static class StartRegisterInternalServices
    {
        public static void StartInternalServicePipe(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceRegistrations(configuration);
            services.AddAppDbContext(configuration);
            services.AddMediatRRegistrations();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            services.AddAutoMapperCustom(configuration);
        }


        private static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomRepositories();
            services.AddCustomServices();
            services.AddEventBus(configuration, services.BuildServiceProvider().GetRequiredService<ILogger<Program>>());
        }

        private static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IBidRoomRepository, BidRoomRepository>();
        }

        private static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IBiddingService, BiddingService>();
        }

        public static void StartInternalServiceApp(this WebApplication app)
        {
            app.SetEnvBasedServices();
            app.MigrateAppDb();
            app.SetControllerConfigs();
            app.Run();
        }

        private static void SetEnvBasedServices(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapGrpcReflectionService();
            }
        } 

        private static void MigrateAppDb(this WebApplication app)
        {
            app.MigrateDatabase<BidDbContext>();
        }

        private static void SetControllerConfigs(this WebApplication app)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQLPlayground("graphql");
                endpoints.MapGraphQLVoyager("ui/voyager");
            });


            app.MapGrpcService<BidGrpcServerService>();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseConsul();
            app.AddGraphQLApp();
        }
    }
}
