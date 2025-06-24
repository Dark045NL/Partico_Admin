namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Part> Parts { get; } = new List<Part>();
    }
}
