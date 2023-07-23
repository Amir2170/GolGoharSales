using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class TransportationsController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    
    //Initializing unitOfWork using injected context
    public TransportationsController(SalesAppContext context)
    {
        _unitOfWork = new UnitOfWork(context);
    }
    
    //GET: transportations/
    // Get all transportations
    [HttpGet]
    public IEnumerable<Transportation> GetAllTransportations()
    {
        return _unitOfWork.TransportationRepository.GetAll();
    }
    
    //GET: transportations/{id}
    // Get transportation by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Transportation> GetTransportationById(int id)
    {
        var transportation = _unitOfWork.TransportationRepository.GetById(id);

        if (transportation == null)
        {
            return NotFound(new { message = "transportation with given id doesn't exists" });
        }

        return transportation;
    }
    
    //PUT: transportations/{id}
    /* Update transportation with id parameter to transportation entity parameter
        return 204 NoContent result if successful
     */
    [HttpPut("{id}")]
    public IActionResult UpdateTransportation(int id, Transportation transportationUpdate)
    {
        // checking if id is equal to transportationUpdate id
        if (id != transportationUpdate.Id)
        {
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get transportation by id parameter
        var transportation = _unitOfWork.TransportationRepository.GetById(id);
        
        // check if transportation exists
        if (transportation == null)
        {
            return NotFound(new { message = "transportation not found" });
        }
        
        // check if salesContractId in the given request body exists if not return a 404 notfound
        var contract = _unitOfWork.SalesContractRepository.GetById(
            transportationUpdate.SalesContractId
            );

        if (contract == null)
        {
            return NotFound(
                new
                {
                    message = "contract with given id does not exists enter a valid contract id"
                });
        }
        
        //update transportation
        transportation.Date = transportationUpdate.Date;
        transportation.SetTonnage = transportationUpdate.SetTonnage;
        transportation.SalesContractId = transportationUpdate.SalesContractId;
        
        //track changes
        _unitOfWork.TransportationRepository.Update(transportation);
        
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
    
    //POST: transportations
    // Creating a transportation 
    [HttpPost]
    public ActionResult<Customer> CreateTransportation(Transportation newTransportation)
    {   
        // do not add duplicate transportation
        if (_unitOfWork.TransportationRepository.TransportationExists(newTransportation))
        {
            return BadRequest(new { message = "transportation already exists" });
        }
        
        // check if contract with id in request body exists
        var contract = _unitOfWork.SalesContractRepository.GetById(
            newTransportation.SalesContractId
            );

        if (contract == null)
        {
            return NotFound(
                new { message = "contract with given id does not exists enter a vlid contract id"}
            );
        }
        
        //create a new transportation
        var transportation = new Transportation
        {
            Date = newTransportation.Date,
            SetTonnage = newTransportation.SetTonnage,
            SalesContractId = newTransportation.SalesContractId
        };
        
        // start tracking
        _unitOfWork.TransportationRepository.Add(transportation);
        
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
        
        // return No content 204 if successful
        return NoContent();
    }
    
    // DELETE: transportations/{id} 
    //deletes transportation with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteTransportationById(int id)
    {
        // get transportation with the the given id
        var transportation = _unitOfWork.TransportationRepository.GetById(id);
        
        // check if transportation exists if not return a notFound response
        if (transportation == null)
        {
            return NotFound(new { message = "transportation not found" });
        }
        
        // start tracking 
        _unitOfWork.TransportationRepository.Remove(transportation);
        
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