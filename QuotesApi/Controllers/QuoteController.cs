﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly QuotesContext _context;

        public QuoteController(QuotesContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<QuoteDisplayDTO>>> GetQuotes()
        {
            //TODO: Move to user service claims extraction???
            string currentUserId = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                //IEnumerable<Claim> claims = identity.Claims;
                // or
                currentUserId = identity
                    .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }

            var quotes = _context.Quotes.Include(q => q.RatingList).ThenInclude(r => r.User);
            List<QuoteGetDTO> result = new List<QuoteGetDTO>();


            foreach (var quote in quotes)
            {
                int positiveCounter = 0;
                int negativeCounter = 0;
                bool? userVote = null;

                quote.RatingList.ForEach(r =>
                {
                    if (r.Positive) positiveCounter++;
                    else negativeCounter++;

                    if (r.User.Id.ToString() == currentUserId)
                        userVote = r.Positive;
                });

                int total = positiveCounter + negativeCounter;
                if (total == 0)
                    total = 1;

                result.Add(new QuoteGetDTO()
                {
                    Quote = quote,
                    NegativeCount = negativeCounter,
                    PositiveCount = positiveCounter,
                    UserVotePositive = userVote,
                    Percentage =
                        (int)Math.Round((double)positiveCounter / ((double)total) * 100),
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Quote>> GetQuoteById(Guid id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote is null)
                return NotFound("Quote with id not found");
            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> AddQuote([FromBody] QuoteDisplayDTO displayDto)
        {
            if (string.IsNullOrEmpty(displayDto.Author) || string.IsNullOrEmpty(displayDto.Content))
                return BadRequest();

            Quote newQuote = new Quote
            {
                Content = displayDto.Content,
                Author = displayDto.Author
            };

            await _context.Quotes.AddAsync(newQuote);
            await _context.SaveChangesAsync();

            return Ok(newQuote);
        }
    }
}