using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();

        public Product? GetProductById(int id);

        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public void DeleteProduct(Product product);
    }
}
