using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuotesApi.Models;

public class Rating
{
    [Key] public Guid Id { get; set; }
    [Required] public bool Positive { get; set; }
    [JsonIgnore]
    [Required] public Quote Quote { get; set; } = null!;
    [Required] public User User { get; set; } = null!;
}