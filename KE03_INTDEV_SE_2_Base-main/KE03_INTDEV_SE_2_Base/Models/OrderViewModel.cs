using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace Partico_Panel.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Selecteer een klant.")]
        public int CustomerId { get; set; }

        public List<Customer> Customers { get; set; } = [];

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public List<ProductSelection> Products { get; set; } = [];
    }

    public class ProductSelection
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
