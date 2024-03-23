namespace QuotesApi.DTOs;

public class QuotesResponseDTO
{
    public List<QuoteDisplayDTO> Quotes { get; set; } = new();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}