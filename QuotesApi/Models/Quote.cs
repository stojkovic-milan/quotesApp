using System.ComponentModel.DataAnnotations;

namespace QuotesApi.Models;

public class Quote
{
    [Key] public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public List<Rating> RatingList { get; set; } = new();
    public int PositiveCount { get; set; } = 0;
    public int NegativeCount { get; set; } = 0;
}