﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuotesApi.DTOs;
using QuotesApi.Models;

namespace QuotesApi.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly QuotesContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IConfiguration config, QuotesContext context, IHttpContextAccessor httpContextAccessor)
    {
        _config = config;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SignupResponseDTO> SignUp(SignupDTO signupDTO)
    {
        var emailInUse = _context.Users
            .Any(p => p.Email == signupDTO.Email);
        if (emailInUse)
            throw new Exception("Email already in use");

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(signupDTO.Password);


        User newUser = new User
        {
            Email = signupDTO.Email,
            PasswordHash = passwordHash
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new SignupResponseDTO
        {
            Token = CreateToken(newUser)
        };
    }

    public async Task<SignupResponseDTO> SignIn(SignupDTO signinDTO)
    {
        var user = await _context.Users.Where(u => u.Email == signinDTO.Email).FirstOrDefaultAsync();

        if (user is null)
            throw new Exception("User not registered");

        if (!BCrypt.Net.BCrypt.Verify(signinDTO.Password, user.PasswordHash))
        {
            throw new Exception("Password not matching.");
        }

        return new SignupResponseDTO
        {
            Token = CreateToken(user)
        };
    }

    public Guid GetCurrentUserId()
    {
        string? currentUserId = "";
        if (_httpContextAccessor.HttpContext?.User.Identity is ClaimsIdentity identity)
        {
            currentUserId = identity
                .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        }

        if (currentUserId is null)
            throw new Exception("User not logged in");

        return Guid.Parse(currentUserId);
    }

    public async Task<User?> GetCurrentUser()
    {
        var currentUserId = GetCurrentUserId();
        var currentUser = await _context.Users.FindAsync(currentUserId);
        return currentUser;
    }

    public string CreateToken(User u)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
            new Claim(ClaimTypes.Email, u.Email),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Auth:Token").Value ??
                                                                              throw new
                                                                                  SecurityTokenInvalidSigningKeyException()));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(8), //8 days for testing
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}