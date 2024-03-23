using QuotesApi.Models;

namespace QuotesApi.DTOs
{
    public class QuoteDisplayDTO
    {
        public Quote Quote { get; set; } = null!;
        public int PositiveCount { get; set; }
        public int NegativeCount { get; set; }
        public int Percentage { get; set; }
        public bool? UserVotePositive { get; set; }
    }
}