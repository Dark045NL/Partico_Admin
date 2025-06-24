using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Selecteer een klant.")]
        public int CustomerId { get; set; }

        public List<Customer> Customers { get; set; } = new();

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public List<ProductSelection> Products { get; set; } = new();
    }

    public class ProductSelection
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
