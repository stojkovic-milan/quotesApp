namespace QuotesApi.DTOs
{
    public class QuoteCreateDTO
    {
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<string> TagList { get; set; } = new();
    }
}