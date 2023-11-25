using Bidder.BidService.Application.GraphQL.GraphSchema;
using Bidder.BidService.Application.GraphQL.Query;
using Bidder.BidService.Application.GraphQL.Types;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;

namespace Bidder.BidService.Api.Registration
{
    public static class GraphQLServices
    {
        public static void AddGraphQLServices(this IServiceCollection services)
        {

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentBuilder  , GraphQLDocumentBuilder>();

            services.AddSingleton<BidRoomUserGraphType>();
            services.AddSingleton<BidRoomGraphType>();
            services.AddSingleton<BidGraphType>();
            services.AddScoped<BidQuery>();


            services.AddScoped<ISchema, BidSchema>();
        }

        public static void AddGraphQLApp(this WebApplication app)
        {
            app.UseGraphQLPlayground("/graphql");
        }
    }
}
