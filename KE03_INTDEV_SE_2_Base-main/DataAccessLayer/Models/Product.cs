namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = [];

        public ICollection<Order> Orders { get; set; } = [];

        public ICollection<Part> Parts { get; } = [];

    }
}
