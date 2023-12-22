namespace AutomatedDataCollectionApi.Models
{
    public class ParsedEntity
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}