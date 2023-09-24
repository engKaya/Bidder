﻿using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Infastructure.Repos;
using Bidder.BidService.Infastructure.Services;
using Bidder.BidService.Infastructure.Uof;
using Bidder.Domain.Common.Interfaces;

namespace Bidder.BidService.Api.Registration
{
    public static class AddServiceRegisterations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCustomRepositories();
            services.AddCustomServices();
            services.AddEventBus(configuration, services.BuildServiceProvider().GetRequiredService<ILogger<Program>>());
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IBaseRepository<>), typeof(Repository<>));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IBidRepository, BidRepository>();
            services.AddSingleton<IBidRoomRepository, BidRoomRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IBiddingService, BiddingService>();
        }
    }
}
