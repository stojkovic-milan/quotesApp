using Microsoft.AspNetCore.Mvc;
using QuotesApi.DTOs;
using QuotesApi.Models;
using Microsoft.AspNetCore.Authorization;
using QuotesApi.Services;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;


        public QuoteController(QuotesContext context, IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<QuotesResponseDTO> GetQuotes(
            [FromQuery] List<string>? filterTags,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5, [FromQuery] SortType? sortType = null)
        {
            var retVal = _quoteService.GetQuotes(filterTags, page, pageSize);
            return Ok(retVal);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Quote>> GetQuoteById(Guid id)
        {
            var quote = await _quoteService.GetQuoteById(id);
            if (quote is null)
                return NotFound("Quote with id not found");
            return Ok(quote);
        }

        [HttpGet]
        [Route("Tags")]
        public async Task<ActionResult<List<string>>> GetAllTags()
        {
            var tags = await _quoteService.GetAllTags();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> AddQuote([FromBody] QuoteCreateDTO quoteDto)
        {
            var newQuote = await _quoteService.AddQuote(quoteDto);
            return Ok(newQuote);
        }
    }
}