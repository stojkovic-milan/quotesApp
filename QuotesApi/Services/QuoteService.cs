using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Services;

public enum SortType
{
    Default,
    RatingAsc,
    RatingDesc,
    RatingNumAsc,
    RatingNumDesc,
    Author,
    Content,
}

public class QuoteService : IQuoteService
{
    private readonly QuotesContext _context;
    private readonly IUserService _userService;

    public QuoteService(QuotesContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public QuotesResponseDTO GetQuotes(List<string>? filterTags, int page = 1, int pageSize = 5,
        SortType? sortType = null)
    {
        string currentUserId = _userService.GetCurrentUserId().ToString();


        var quotesQuery = _context.Quotes.Include(q => q.RatingList).ThenInclude(r => r.User).AsQueryable();

        //Filtering
        if (filterTags is not null && filterTags.Count > 0)
        {
            quotesQuery = quotesQuery.Where(q => q.Tags.Any(t => filterTags.Contains(t)));
        }

        //Sorting
        if (sortType is not null && sortType != SortType.Default)
        {
            IOrderedQueryable<Quote> sortedQuery;
            switch (sortType)
            {
                case SortType.Author:
                    sortedQuery = quotesQuery.OrderBy(q => q.Author);
                    break;
                case SortType.Content:
                    sortedQuery = quotesQuery.OrderBy(q => q.Content);
                    break;
                case SortType.RatingAsc:
                    sortedQuery = quotesQuery.OrderBy(q =>
                        (int)Math.Round((double)q.PositiveCount /
                                        ((double)((q.NegativeCount + q.PositiveCount) > 0
                                            ? q.NegativeCount + q.PositiveCount
                                            : 1)) *
                                        100)).ThenBy(q => q.PositiveCount);
                    break;
                case SortType.RatingDesc:
                    sortedQuery = quotesQuery.OrderByDescending(q =>
                        (int)Math.Round((double)q.PositiveCount /
                                        ((double)((q.NegativeCount + q.PositiveCount) > 0
                                            ? q.NegativeCount + q.PositiveCount
                                            : 1)) *
                                        100)).ThenBy(q => q.NegativeCount);
                    break;
                case SortType.RatingNumAsc:
                    sortedQuery = quotesQuery.OrderBy(q => q.NegativeCount + q.PositiveCount);
                    break;
                default: //SortType.RatingNumDesc
                    sortedQuery = quotesQuery.OrderByDescending(q => q.NegativeCount + q.PositiveCount);
                    break;
            }

            quotesQuery = sortedQuery;
        }

        //Pagination
        var totalCount = quotesQuery.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        quotesQuery = quotesQuery.Skip((page - 1) * pageSize).Take(pageSize);

        var quotes = quotesQuery.AsEnumerable();

        List<QuoteDisplayDTO> result = new();


        foreach (var quote in quotes)
        {
            //Check for current user's rating
            bool? userVote = null;
            userVote = quote.RatingList.FirstOrDefault(r => r.User.Id.ToString() == currentUserId)?.Positive;

            result.Add(GetQuoteDisplay(quote, userVote));
        }


        var retVal = new QuotesResponseDTO
        {
            Quotes = result,
            CurrentPage = page,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        return retVal;
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        var quote = await _context.Quotes.FindAsync(id);
        return quote;
    }

    public async Task<IEnumerable<string>> GetAllTags()
    {
        IEnumerable<string> tags = Enumerable.Empty<string>();
        await _context.Quotes.ForEachAsync(q => { tags = tags.Union(q.Tags); });

        return tags;
    }

    public async Task<Quote> AddQuote(QuoteCreateDTO quoteDto)
    {
        if (string.IsNullOrEmpty(quoteDto.Author))
            throw new Exception("Quote author invalid");
        if (string.IsNullOrEmpty(quoteDto.Content))
            throw new Exception("Quote content invalid");

        Quote newQuote = new Quote
        {
            Content = quoteDto.Content,
            Author = quoteDto.Author,
            Tags = quoteDto.TagList
        };

        await _context.Quotes.AddAsync(newQuote);
        await _context.SaveChangesAsync();

        return newQuote;
    }

    public QuoteDisplayDTO GetQuoteDisplay(Quote quote, bool? userVote)
    {
        var total = (double)quote.PositiveCount + quote.NegativeCount;
        if (total == 0)
            total = 1;

        var retVal = new QuoteDisplayDTO()
        {
            Quote = quote,
            UserVotePositive = userVote,
            Percentage =
                (int)Math.Round(quote.PositiveCount / total * 100),
        };

        return retVal;
    }
}