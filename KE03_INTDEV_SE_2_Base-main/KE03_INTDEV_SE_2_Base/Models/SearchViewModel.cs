using System.Collections.Generic;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<SearchResult> Results { get; set; } = new List<SearchResult>();
    }

    public class SearchResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }  // "Product", "Klant", or "Onderdeel"
        public string Url { get; set; }
    }
} 