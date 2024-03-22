using System.ComponentModel.DataAnnotations;

namespace QuotesApi.Models;

public class Rating
{
    [Key] public Guid Id { get; set; }
    [Required] public bool Positive { get; set; }
    [Required] public Quote Quote { get; set; } = null!;
    [Required] public User User { get; set; } = null!;
}