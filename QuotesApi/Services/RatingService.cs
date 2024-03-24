using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly QuotesContext _context;
        private readonly IUserService _userService;
        private readonly IQuoteService _quoteService;

        public RatingService(QuotesContext context, IUserService userService, IQuoteService quoteService)
        {
            _context = context;
            _userService = userService;
            _quoteService = quoteService;
        }

        public async Task<QuoteDisplayDTO> RateQuote(RatingDTO ratingDTO)
        {
            var user = await _userService.GetCurrentUser();
            if (user is null)
                throw new Exception("User not logged in");

            var quote = await _quoteService.GetQuoteById(ratingDTO.QuoteId);
            if (quote is null)
                throw new Exception("Quote not found");

            //Check if user already rated quote
            var currentRating = await _context.Ratings.Include(r => r.User)
                .Include(r => r.Quote)
                .Where(r => r.Quote.Id == ratingDTO.QuoteId && r.User.Id == user.Id)
                .FirstOrDefaultAsync();
            bool? newRatingPositive;
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (currentRating is not null)
                {
                    if (currentRating.Positive == ratingDTO.Positive)
                    {
                        _context.Ratings.Remove(currentRating);
                        newRatingPositive = null;
                        if (currentRating.Positive)
                            quote.PositiveCount--;
                        else
                            quote.NegativeCount--;
                    }
                    else
                    {
                        currentRating.Positive = ratingDTO.Positive;
                        newRatingPositive = ratingDTO.Positive;
                        if (ratingDTO.Positive)
                        {
                            quote.NegativeCount--;
                            quote.PositiveCount++;
                        }
                        else
                        {
                            quote.NegativeCount++;
                            quote.PositiveCount--;
                        }
                    }
                }
                else
                {
                    Rating newRating = new Rating
                    {
                        Positive = ratingDTO.Positive,
                        User = user,
                        Quote = quote
                    };
                    await _context.Ratings.AddAsync(newRating);
                    if (ratingDTO.Positive)
                        quote.PositiveCount++;
                    else
                        quote.NegativeCount++;

                    currentRating = newRating;
                    newRatingPositive = newRating.Positive;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }

            var retVal = _quoteService.GetQuoteDisplay(quote, newRatingPositive);

            return retVal;
        }
    }
}