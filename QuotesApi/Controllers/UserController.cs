using Microsoft.AspNetCore.Mvc;
using QuotesApi.DTOs;
using QuotesApi.Services;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; set; }


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult<SignupResponseDTO>> SignUp([FromBody] SignupDTO dto)
        {
            var response = await _userService.SignUp(dto);
            return Ok(response);
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<ActionResult<SignupResponseDTO>> SignIn([FromBody] SignupDTO dto)
        {
            var response = await _userService.SignIn(dto);
            return Ok(response);
        }
    }
}