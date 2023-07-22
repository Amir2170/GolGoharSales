using System.Linq;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.Customers;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;
using Microsoft.EntityFrameworkCore;

namespace GolGoharSales.Data.CustomerRepositoryNS;

public class CustomerRepository : GenericRepository<Customer> , ICustomerRepository
{
    // supplying injected context to base class
    public CustomerRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if customer with given title , telephone , address exists
      ids are always different in database */
    public bool CustomerExists(Customer customer)
    {
        return Context.Customers.Any(dbCustomer =>
            dbCustomer.Title == customer.Title &&
            dbCustomer.Telephone == customer.Telephone &&
            dbCustomer.Address == customer.Address);
    }
}