using Newtonsoft.Json.Linq;

namespace Bidder.Application.Common.GraphQLData
{
    public class GraphqlQueryParameter
    {
        public string OperationName { get; set; } 
        public string Query { get; set; }
        public JObject Variables { get; set; }

    }
}
