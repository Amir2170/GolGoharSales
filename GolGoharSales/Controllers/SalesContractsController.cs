using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesContractsController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    
    //Initializing unitOfWork using injected context
    public SalesContractsController(SalesAppContext context)
    {
        _unitOfWork = new UnitOfWork(context);
    }
    
    //GET: salesContracts/
    // Get all contracts
    [HttpGet]
    public IEnumerable<SalesContract> GetAllContracts()
    {
        return _unitOfWork.SalesContractRepository.GetAll();
    }
    
    //GET: salesContracts/{id}
    // Get contracts by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<SalesContract> GetContractById(int id)
    {
        var contract = _unitOfWork.SalesContractRepository.GetById(id);

        if (contract == null)
        {
            return NotFound(new { message = "contract with given id doesn't exists" });
        }

        return contract;
    }
    
    //PUT: salesContracts/{id}
    /* Update contract with id parameter to contract entity parameter
        return 204 NoContent result if successful
     */
    [HttpPut("{id}")]
    public IActionResult UpdateContract(int id, SalesContract contractUpdate)
    {
        // checking if id is equal to contractUpdate id
        if (id != contractUpdate.Id)
        {
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get contract by id parameter
        var contract = _unitOfWork.SalesContractRepository.GetById(id);
        
        // check if contract exists
        if (contract == null)
        {
            return NotFound(new { message = "contract not found" });
        }
        
        // check if production with the given id in contractUpdate body exists
        var production = _unitOfWork.ProductionRepository.GetById(contractUpdate.ProductionId);

        if (production == null)
        {
            return NotFound(
                new { message = "production with given id does not exists enter a valid production id" }
            );
        }
        
        // check if customer with the given id in contractUpdate body exists
        var customer = _unitOfWork.CustomerRepository.GetById(contractUpdate.CustomerId);

        if (customer == null)
        {
            return NotFound(
                new { message = "customer with given id does not exists enter a valid customer id" }
            );
        }
        
        //update contract
        contract.CustomerId = contractUpdate.CustomerId;
        contract.ProductionId = contractUpdate.ProductionId;
        contract.Value = contractUpdate.Value;
        contract.Monthly = contractUpdate.Monthly;
        contract.ContractNumber = contractUpdate.ContractNumber;
        contract.StartDate = contractUpdate.StartDate;
        contract.ContractDate = contractUpdate.ContractDate;
        contract.FinishDate = contractUpdate.FinishDate;

        //track changes
        _unitOfWork.SalesContractRepository.Update(contract);
        
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
    
    //POST: salesContracts
    // Creating a contract 
    [HttpPost]
    public ActionResult<SalesContract> CreateContract(SalesContract newContract)
    {   
        // do not add duplicate contracts
        if (_unitOfWork.SalesContractRepository.ContractExists(newContract))
        {
            return BadRequest(new { message = "contract already exists" });
        }
        
        // check if production with given id exists if not return a 404 not found
        var production = _unitOfWork.ProductionRepository.GetById(newContract.ProductionId);

        if (production == null)
        {
            return NotFound(
                new { message = "production with given id does not exists enter a valid production id" }
            );
        }
        
        // check if customer with given id exists if not return a 404 not found
        var customer = _unitOfWork.CustomerRepository.GetById(newContract.CustomerId);

        if (customer == null)
        {
            return NotFound(
                new { message = "customer with given id does not exists enter a valid production id" }
            );
        }
        
        //create a new contract
        var contract = new SalesContract
        {
            ContractNumber = newContract.ContractNumber,
            ContractDate = newContract.ContractDate,
            StartDate = newContract.StartDate,
            FinishDate = newContract.FinishDate,
            Monthly = newContract.Monthly,
            Value = newContract.Value,
            ProductionId = newContract.ProductionId,
            CustomerId = newContract.CustomerId
        };
        
        // start tracking
        _unitOfWork.SalesContractRepository.Add(contract);
        
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
        
        // return No Content 204 if successful
        return NoContent();
    }
    
    // DELETE: SalesContract/{id} 
    //deletes contracts with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteContractById(int id)
    {
        // get contract with the the given id
        var contract = _unitOfWork.SalesContractRepository.GetById(id);
        
        // check if contract exists if not return a notFound response
        if (contract == null)
        {
            return NotFound(new { message = "contract not found" });
        }
        
        // start tracking 
        _unitOfWork.SalesContractRepository.Remove(contract);
        
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