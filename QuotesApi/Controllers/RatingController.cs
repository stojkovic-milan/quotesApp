using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly QuotesContext _context;

        public RatingController(QuotesContext context)
        {
            _context = context;
        }

        [HttpPut]
        public async Task<ActionResult<List<Rating>>> RateQuote([FromBody] RatingDTO dto)
        {
            //TODO:Get userid from claim instead of dto, to avoid voting for other users based on id
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user is null)
                return BadRequest("Voting user not found");

            var quote = await _context.Quotes.FindAsync(dto.QuoteId);
            if (quote is null)
                return BadRequest("Quote not found");

            //Check if user already rated quote
            var currentRating = await _context.Ratings.Where(r => r.Quote.Id == dto.QuoteId && r.User.Id == dto.UserId)
                .FirstOrDefaultAsync();

            if (currentRating is not null)
            {
                currentRating.Positive = dto.Positive;
            }
            else
            {
                Rating newRating = new Rating
                {
                    Positive = dto.Positive,
                    User = user,
                    Quote = quote
                };
                await _context.Ratings.AddAsync(newRating);
            }

            await _context.SaveChangesAsync();

            var quoteRatings = _context.Ratings.Where(r => r.Quote.Id == dto.QuoteId);
            return Ok(quoteRatings);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveRating(Guid id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating is null)
                return NotFound("Rating not found");

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}