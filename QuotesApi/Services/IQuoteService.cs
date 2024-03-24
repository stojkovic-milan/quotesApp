using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Services
{
    public interface IQuoteService
    {
        public QuotesResponseDTO GetQuotes(List<string>? filterTags, int page = 1, int pageSize = 5,
            SortType? sortType = null);

        public Task<Quote?> GetQuoteById(Guid id);
        public Task<IEnumerable<string>> GetAllTags();
        public Task<Quote> AddQuote(QuoteCreateDTO quoteDto);
        public QuoteDisplayDTO GetQuoteDisplay(Quote quote, bool? userVote);
    }
}