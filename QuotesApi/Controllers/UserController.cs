using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuotesApi.DTOs;
using QuotesApi.Models;
using QuotesApi.Services;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; set; }


        public UserController(QuotesContext context, IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult<User>> GetUserById(Guid id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user is null)
        //        return NotFound("User with id not found");
        //    return Ok(user);
        //}

        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult<SignupResponseDTO>> SignUp([FromBody] SignupDTO dto)
        {
            //if (string.IsNullOrEmpty(dto.Author) || string.IsNullOrEmpty(dto.Content))
            //    return BadRequest();
            var response = await _userService.SignUp(dto);
            return Ok(response);
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<ActionResult<SignupResponseDTO>> SignIn([FromBody] SignupDTO dto)
        {
            //if (string.IsNullOrEmpty(dto.Author) || string.IsNullOrEmpty(dto.Content))
            //    return BadRequest();
            var response = await _userService.SignIn(dto);
            return Ok(response);
        }
    }
}