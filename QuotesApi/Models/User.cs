using System.ComponentModel.DataAnnotations;

namespace QuotesApi.Models;

public class User
{
    [Key] public Guid Id { get; set; }
    [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string PasswordHash { get; set; } = string.Empty;
    public List<Rating> Ratings { get; set; } = new();
}