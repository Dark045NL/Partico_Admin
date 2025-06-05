using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetAllCustomers();

        public Customer? GetCustomerById(int id);

        public void AddCustomer(Customer customer);

        public void UpdateCustomer(Customer customer);

        public void DeleteCustomer(Customer customer);
    }
}
