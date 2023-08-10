using Bidder.IdentityService.Application.Features.Commands.User.CreateUser;
using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Interfaces;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Repos;
using Bidder.IdentityService.Infastructure.Uof;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Reflection;

namespace Bidder.IdentityService.Api.Registration
{
    public static class CustomServiceRegistration
    {
        public static  IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")); 
                options.EnableSensitiveDataLogging();
            });
            services.AddMediatR();
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>()
                                                .UseSqlServer(configuration.GetConnectionString("UserConnectionString"));

            //using var dbContext = services.BuildServiceProvider().GetService<UserDbContext>();
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();

            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            services.ConfigureAuth(configuration);
            services.ConnectToMessaBroker(configuration);
            return services;
        }

        public static void AddMediatR(this IServiceCollection services)
        { 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void ConnectToMessaBroker(this IServiceCollection services, IConfiguration conf)
        {
            var _uri = conf["RabbitSettings:uri"];
            var _hostname = conf["RabbitSettings:host"];
            var _port = int.Parse(conf["RabbitSettings:port"]);
            var _username = conf["RabbitSettings:username"];
            var _password = conf["RabbitSettings:password"];
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig conf = new()
                {
                    ConnectionRetry = 5,
                    SubscriberClientAppName = "Bidder.IdentityService",
                    DefaultTopicName = "Bidder",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    Connection = new ConnectionFactory()
                    {
                        HostName = _hostname,
                        Port = _port,
                        UserName = _username,
                        Password = _password,
                        Uri = new Uri(_uri)
                    }
                };
                return EventBusFactory.CreateEventBus(conf, sp);
            });
            var provider = services.BuildServiceProvider();
            var eventBus = provider.GetRequiredService<IEventBus>();
        }
    }
}
