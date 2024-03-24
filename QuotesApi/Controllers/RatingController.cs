﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesApi.DTOs;
using System.Security.Claims;
using QuotesApi.Services;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }


        [HttpPost]
        public async Task<ActionResult<QuoteDisplayDTO>> RateQuote([FromBody] RatingDTO ratingDTO)
        {
            var retVal = await _ratingService.RateQuote(ratingDTO);
            return Ok(retVal);
        }
    }
}