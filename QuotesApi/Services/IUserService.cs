using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Services
{
    public interface IUserService
    {
        public Task<SignupResponseDTO> SignUp(SignupDTO signupDTO);
        public Task<SignupResponseDTO> SignIn(SignupDTO signinDTO);
    }
}