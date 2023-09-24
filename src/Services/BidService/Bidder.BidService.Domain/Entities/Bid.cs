using Bidder.Domain.Common.Entity;

namespace Bidder.BidService.Domain.Entities
{
    public class Bid : BaseEntity
    { 
        private Guid _userId;
        private Guid? _categoryId;
        private DateTime _endDate;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private decimal? _minPrice;
        private bool _isEnded = false;
        private bool _hasIncreaseRest = false;
        private decimal _minPriceIncrease;
        private int _productType = 0;
         
        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public int ProductType { get => _productType; set => _productType = value; }
        public decimal? MinPrice { get => _minPrice; set => _minPrice = value; }
        public bool IsEnded { get => _isEnded; set => _isEnded = value; }
        public bool HasIncreaseRest { get => _hasIncreaseRest; set => _hasIncreaseRest= value; }
        public decimal MinPriceIncrease { get => _minPriceIncrease; set => _minPriceIncrease = value; }
        public Guid UserId { get => _userId; set => _userId = value; }
        public Guid? CategoryId { get => _categoryId; set => _categoryId = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public BidRoom BidRoom { get; set; } = null!;
        public Bid(string title, string description, decimal? minPrice, bool hasIncreaseRest, decimal minPriceIncrease, Guid  userId)
        {
            Id  = Guid.NewGuid();
            _title = title;
            _description = description;
            _minPrice = minPrice;
            _hasIncreaseRest = hasIncreaseRest;
            _minPriceIncrease = minPriceIncrease;
            _userId = userId;
            _endDate = DateTime.Now.AddDays(7);
        }
    }
}
