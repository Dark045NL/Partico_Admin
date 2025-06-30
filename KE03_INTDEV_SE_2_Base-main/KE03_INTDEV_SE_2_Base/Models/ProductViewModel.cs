using DataAccessLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Partico_Panel.Models
{
    public class ProductViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public List<int> SelectedPartIds { get; set; } = new List<int>();

        public List<Part> AllParts { get; set; } = new List<Part>();
    }

}
