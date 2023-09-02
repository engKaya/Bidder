using AutoMapper;
using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.BidService.Application.Mapping
{
    public static class AutoMapperConfigure
    {
        public static void AddAutoMapperCustom(this IServiceCollection services, IConfiguration conf)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<CreateBidRequest, CreateBidCommand>();
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
