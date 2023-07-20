using System.Collections.Generic;
using GolGoharSales.Models;
using GolGoharSales.Data;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private UnitOfWork unitOfWork;
    
    //Initializing unitOfWork using injected context
    public CustomersController(SalesAppContext context)
    {
        unitOfWork = new UnitOfWork(context);
    }
    
    //GET: customers/
    // Get all customers
    [HttpGet]
    public IEnumerable<Customer> GetAllCustomers()
    {
        return unitOfWork.CustomerRepository.GetAll();
    }
    
    //GET: customers/{id}
    // Get customer by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Customer> GetCustomerById(int id)
    {
        var customer = unitOfWork.CustomerRepository.GetById(id);

        if (customer == null)
        {
            return NotFound(new { message = "Customer with given id doesn't exists" });
        }

        return customer;
    }
    
    //PUT: customers/{id}
    /* Update customer with id parameter to customer entity parameter
        return 204 NoContent result if successful
     */
    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, Customer customerUpdate)
    {
        // checking if id is equal to customerUpdate id
        if (id != customerUpdate.Id)
        {
            return BadRequest(new { message = "Id in url in not equal to id in request body" });
        }
        
        // get customer by id parameter
        var customer = unitOfWork.CustomerRepository.GetById(id);
        
        // check if customer exists
        if (customer == null)
        {
            return NotFound(new { message = "Customer not found" });
        }
        
        //update customer
        customer.Title = customerUpdate.Title;
        customer.Address = customerUpdate.Address;
        customer.Telephone = customerUpdate.Telephone;
        
        //track changes
        unitOfWork.CustomerRepository.Update(customer);
        
        // savechanges exeption handling
        try
        {
            unitOfWork.Save();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return NoContent();
    }
}