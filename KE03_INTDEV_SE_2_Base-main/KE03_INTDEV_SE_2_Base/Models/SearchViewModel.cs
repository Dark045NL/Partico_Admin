namespace Partico_Panel.Models
{
    public class SearchViewModel
    {
        public string? Query { get; set; }
        public List<SearchResult> Results { get; set; } = [];
    }

    public class SearchResult
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
    }
}