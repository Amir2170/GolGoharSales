using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.Customers;

public interface ICustomerRepository : IGenericRepository<Customer>
{   
    // check if customer exists
    bool CustomerExists(Customer customer);
}