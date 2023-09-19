export class CreateBidRequest {
    Title: string;
    Description: string;
    MinPrice: number;
    HasIncreaseRest: boolean;
    MinPriceIncrease: number;
    
    /**
     *
     */
    constructor(title: string, description: string, minPrice: number, hasIncreaseRest: boolean, minPriceIncrease: number) {
        this.Title = title;
        this.Description = description;
        this.MinPrice = minPrice;
        this.HasIncreaseRest = hasIncreaseRest;
        this.MinPriceIncrease = minPriceIncrease;
    }
}