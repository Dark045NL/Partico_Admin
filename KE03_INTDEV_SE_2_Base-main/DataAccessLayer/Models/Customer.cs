using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public bool Active { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}