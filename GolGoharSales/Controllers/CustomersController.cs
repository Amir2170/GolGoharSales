using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using GolGoharSales.Models;
using GolGoharSales.Data;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Contract = System.Diagnostics.Contracts.Contract;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    
    //Initializing unitOfWork using injected context
    public CustomersController(SalesAppContext context)
    {
        _unitOfWork = new UnitOfWork(context);
    }
    
    //GET: customers/
    // Get all customers
    [HttpGet]
    public IEnumerable<Customer> GetAllCustomers()
    {
        return _unitOfWork.CustomerRepository.GetAll();
    }
    
    //GET: customers/{id}
    // Get customer by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Customer> GetCustomerById(int id)
    {
        var customer = _unitOfWork.CustomerRepository.GetById(id);

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
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get customer by id parameter
        var customer = _unitOfWork.CustomerRepository.GetById(id);
        
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
        _unitOfWork.CustomerRepository.Update(customer);
        
        // savechanges exeption handling
        try
        {
            _unitOfWork.Save();
        }
        //handling DBConcurrencyException
        catch (DBConcurrencyException ex)
        {
            const string msg = "multiple PUT requests at the same time";
            return StatusCode((int)HttpStatusCode.InternalServerError, msg);
        }
        // handling other error types
        catch (Exception exception)
        {
            // in production throw errors for debug purposes
            Console.WriteLine(exception);
            throw;
            //const string msg = "Unable to update item due to database exceptions";
            //return StatusCode((int)HttpStatusCode.InternalServerError, msg);
        }

        return NoContent();
    }
    
    //POST: customers
    // Creating a customer 
    [HttpPost]
    public ActionResult<Customer> CreateCustomer(Customer newCustomer)
    {   
        // do not add duplicate customers
        if (_unitOfWork.CustomerRepository.CustomerExists(newCustomer))
        {
            return BadRequest(new { message = "customer already exists" });
        }
        
        //create a new customer
        var customer = new Customer
        {
            Title = newCustomer.Title,
            Address = newCustomer.Address,
            Telephone = newCustomer.Telephone
        };
        
        // start tracking
        _unitOfWork.CustomerRepository.Add(customer);
        
        // handle savechanges() exceptions
        try
        {
            _unitOfWork.Save();
        }
        catch (Exception exception)
        {
            // throw exception in production for debug purposes
            Console.WriteLine(exception);
            throw;
        }
        
        // return 204 no content if successful
        return NoContent();
    }
    
    // DELETE: customers/{id} 
    //deletes customer with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomerById(int id)
    {
        // get customer with the the given id
        var customer = _unitOfWork.CustomerRepository.GetById(id);
        
        // check if customer exists if not return a notFound response
        if (customer == null)
        {
            return NotFound(new { message = "customer not found" });
        }
        
        // start tracking 
        _unitOfWork.CustomerRepository.Remove(customer);
        
        // savechanges exception handling
        try
        {
            _unitOfWork.Save();
        }
        catch (Exception exception)
        {
            // throw the exception for debug purposes
            Console.WriteLine(exception);
            throw;
        }

        return NoContent();
    }
}