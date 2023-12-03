using Bidder.BidService.Application.GraphQL.GraphSchema;
using Bidder.BidService.Application.GraphQL.Query;
using Bidder.BidService.Application.GraphQL.Types;
using Bidder.BidService.Application.Interfaces.Utils;
using Bidder.BidService.Infastructure.Utils;
using GraphQL;
using GraphQL.Types;
namespace Bidder.BidService.Api.Registration
{
    public static class GraphQLServices
    {
        public static void AddGraphQLServices(this IServiceCollection services)
        {

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>(); 
            services.AddSingleton<IDocumentWriter, DocumentWriter>(); 
           
            services.AddScoped<BidRoomUserGraphType>();
            services.AddScoped<BidRoomGraphType>();
            services.AddScoped<BidGraphType>();
            services.AddScoped<BidQuery>();
            services.AddScoped<ISchema, BidSchema>();
            services.AddGraphQL(builder => builder
            .AddSystemTextJson() 
            .AddSchema<BidSchema>()
            .AddGraphTypes(typeof(BidQuery).Assembly));

        }

        public static void AddGraphQLApp(this WebApplication app)
        { 
            app.UseGraphQL<ISchema>();
            app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
            app.UseGraphQLPlayground(
                "/",                               // url to host Playground at
                new GraphQL.Server.Ui.Playground.PlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
                    SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
                });
        }
    }
}
