using Microsoft.AspNetCore.Mvc;
using QuotesApi.DTOs;

namespace QuotesApi.Services
{
    public interface IRatingService
    {
        public Task<QuoteDisplayDTO> RateQuote(RatingDTO ratingDTO);
    }
}