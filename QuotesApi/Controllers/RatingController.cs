using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;
using System.Security.Claims;

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


        [HttpPost]
        public async Task<ActionResult<QuoteCreateDTO>> RateQuote([FromBody] RatingDTO ratingDTO)
        {
            string currentUserId = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                currentUserId = identity
                    .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }

            var user = await _context.Users.FindAsync(Guid.Parse(currentUserId));
            if (user is null)
                return BadRequest("Voting user not found");

            var quote = await _context.Quotes.FindAsync(ratingDTO.QuoteId);
            if (quote is null)
                return BadRequest("Quote not found");

            //Check if user already rated quote
            var currentRating = await _context.Ratings.Include(r => r.User)
                .Include(r => r.Quote)
                .Where(r => r.Quote.Id == ratingDTO.QuoteId && r.User.Id == user.Id)
                .FirstOrDefaultAsync();

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (currentRating is not null)
                {
                    if (currentRating.Positive == ratingDTO.Positive)
                    {
                        _context.Ratings.Remove(currentRating);
                        if (currentRating.Positive)
                            quote.PositiveCount--;
                        else
                            quote.NegativeCount--;
                    }
                    else
                    {
                        currentRating.Positive = ratingDTO.Positive;
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
                }

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return BadRequest(e);
            }


            //TODO: Call QuoteService GetquotedisplayById method here

            int total = quote.PositiveCount + quote.NegativeCount;
            if (total == 0)
                total = 1;

            var retVal = new QuoteDisplayDTO()
            {
                Quote = quote,
                UserVotePositive = currentRating.Positive,
                Percentage =
                    (int)Math.Round((double)quote.PositiveCount / ((double)total) * 100),
            };

            return Ok(retVal);
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