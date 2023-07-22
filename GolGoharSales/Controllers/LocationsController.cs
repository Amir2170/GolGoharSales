using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Net;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    
    // initializing context 
    public LocationsController(SalesAppContext context)
    {
        _unitOfWork = new UnitOfWork(context);
    }
    
    //GET: Locations/
    // get all locations
    [HttpGet]
    public IEnumerable<Location> GetAllLocations()
    {
        return _unitOfWork.LocationRepository.GetAll();
    }
    
    //GET: Locations/{id}
    // get location by id if it does not exists return 404 notfound
    [HttpGet("{id}")]
    public ActionResult<Location> GetLocationById(int id)
    {   
        // get location entity
        var location = _unitOfWork.LocationRepository.GetById(id);
        
        // check if location exists if not return a 404 response
        if (location == null)
        {
            return NotFound("location doesn't exists");
        }

        return location;
    }
    
    // PUT: Locations/{id}
    // update location entity with given id
    [HttpPut("{id}")]
    public IActionResult UpdateLocation(int id, Location locationUpdate)
    {
        // checking if id is equal to locationUpdate id
        if (id != locationUpdate.Id)
        {
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get location by id parameter
        var location = _unitOfWork.LocationRepository.GetById(id);
        
        // check if location exists
        if (location == null)
        {
            return NotFound(new { message = "location not found" });
        }
        
        //update location
        location.Title = locationUpdate.Title;

        //track changes
        _unitOfWork.LocationRepository.Update(location);
        
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
    
    //POST: locations
    // Creating a location
    [HttpPost]
    public ActionResult<Location> CreateLocation(Location newLocation)
    {
        // do not add duplicate locations
        if (_unitOfWork.LocationRepository.LocationExists(newLocation))
        {
            return BadRequest(new { message = "location already exists" });
        }
        
        //create a new location
        var location = new Location
        {
            Title = newLocation.Title
        };
        
        // start tracking
        _unitOfWork.LocationRepository.Add(location);
        
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
        
        // return GetItemById with newly created customer
        return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
    }
    
    // DELETE: locations/{id} 
    //deletes location with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteLocationById(int id)
    {
        // get location with the the given id
        var location = _unitOfWork.LocationRepository.GetById(id);
        
        // check if location exists if not return a notFound response
        if (location == null)
        {
            return NotFound(new { message = "location not found" });
        }
        
        // start tracking 
        _unitOfWork.LocationRepository.Remove(location);
        
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