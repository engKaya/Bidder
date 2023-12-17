using Bidder.BidService.Domain.Entities;
using GraphQL.Types;

namespace Bidder.BidService.Application.GraphQL.Types
{
    [Serializable]
    public class BidGraphType : ObjectGraphType<Bid>
    {
        public BidGraphType()
        {
            Name = "Bid";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the bid object.");
            Field(x => x.Title).Description("Title property from the bid object.");
            Field(x => x.Description).Description("Description property from the bid object.");
            Field(x => x.ProductType).Description("ProductType property from the bid object.");
            Field(x => x.MinPrice, nullable: true).Description("MinPrice property from the bid object.");
            Field(x => x.IsEnded).Description("IsEnded property from the bid object.");
            Field(x => x.HasIncreaseRest).Description("HasIncreaseRest property from the bid object.");
            Field(x => x.MinPriceIncrease).Description("MinPriceIncrease property from the bid object.");
            Field(x => x.UserId).Description("UserId property from the bid object.");
            Field(x => x.CategoryId, nullable: true).Description("CategoryId property from the bid object.");
            Field(x => x.EndDate).Description("EndDate property from the bid object.");

        }
    }
}
