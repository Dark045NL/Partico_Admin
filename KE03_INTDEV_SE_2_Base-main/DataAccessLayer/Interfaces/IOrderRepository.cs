using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders();

        public Order? GetOrderById(int id);

        public void AddOrder(Order order);

        public void UpdateOrder(Order order);

        public void DeleteOrder(Order order);
    }
}
