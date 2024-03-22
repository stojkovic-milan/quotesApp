namespace QuotesApi.DTOs
{
    public class RatingDTO
    {
        public bool Positive { get; set; }
        public Guid QuoteId { get; set; }
    }
}
