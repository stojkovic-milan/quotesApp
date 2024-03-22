using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using QuotesApi.Models;

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

        [HttpGet]
        public async Task<ActionResult<List<Quote>>> GetQuotes()
        {
            var quotes = await _context.Quotes.ToListAsync();
            return Ok(quotes);
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
        public async Task<ActionResult> AddQuote([FromBody] QuoteDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Author) || string.IsNullOrEmpty(dto.Content))
                return BadRequest();

            Quote newQuote = new Quote
            {
                Content = dto.Content,
                Author = dto.Author
            };

            await _context.Quotes.AddAsync(newQuote);
            await _context.SaveChangesAsync();

            return Ok(newQuote);
        }
    }
}