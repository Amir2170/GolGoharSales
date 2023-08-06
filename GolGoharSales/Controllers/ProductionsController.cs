using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Net;
using AutoMapper;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;
using Microsoft.AspNetCore.Cors;


namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductionsController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    
    // Initializing unitOfWork and automapper 
    public ProductionsController(
        SalesAppContext context, IMapper mapper
        )
    {
        _unitOfWork = new UnitOfWork(context);
        _mapper = mapper;
    }
    
    //GET: productions/
    // Get all productions
    [HttpGet]
    public IEnumerable<Production> GetAllProductions()
    {
        return _unitOfWork.ProductionRepository.GetAll();
    }
    
    //GET: productions/{id}
    // Get production by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Production> GetProductionById(int id)
    {
        var production = _unitOfWork.ProductionRepository.GetById(id);

        if (production == null)
        {
            return NotFound(new { message = "production with given id doesn't exists" });
        }

        return production;
    }
    
    //PUT: productions/{id}
    /* Update production with id parameter to production entity parameter
        return 204 NoContent result if successful
     */
    [HttpPut("{id}")]
    public IActionResult UpdateProduction(int id, Production productionUpdate)
    {
        // checking if id is equal to production id
        if (id != productionUpdate.Id)
        {
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get production by id parameter
        var production = _unitOfWork.ProductionRepository.GetById(id); 
        
        // check if production exists
        if (production == null)
        {
            return NotFound(new { message = "production not found" });
        }
        
        // check if warehouse with given id exists if not return a warehouse not found error
        var warehouse = _unitOfWork.WarehouseRepository.GetById(productionUpdate.WarehouseId);

        if (warehouse == null)
        {
            return NotFound(new { message = "warehouse with given id does not exists use a valid warehouse id" });
        }
        
        //update production
        production.Title = productionUpdate.Title;
        production.Code = productionUpdate.Code;
        production.StrategicResource = productionUpdate.StrategicResource;
        production.WarehouseId = productionUpdate.WarehouseId;
        
        //track changes
        _unitOfWork.ProductionRepository.Update(production);
        
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
    
    //POST: productions
    // Creating a production and if production exists in a warehouse do not add that again
    [HttpPost]
    public ActionResult<Production> CreateProduction(ProductionDTO newProductionDTO)
    {
        // mapping DTO to production object
        var newProduction = _mapper.Map<ProductionDTO, Production>(newProductionDTO);
        
        // do not add duplicate productions to a similar warehouse
        if (_unitOfWork.ProductionRepository.ProductionExists(newProduction))
        {
            return BadRequest(new
            {
                message = $"production already exists in warehouse {newProduction.WarehouseId}"
            });
        }
        
        // check if warehouse with given id exists if not return a warehouse not found error
        var warehouse = _unitOfWork.WarehouseRepository.GetById(newProduction.WarehouseId);

        if (warehouse == null)
        {
            return NotFound(new { message = "warehouse with given id does not exists use a valid warehouse id" });
        }
        
        // start tracking
        _unitOfWork.ProductionRepository.Add(newProduction);
        
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
        
        // return 204 if successfully created production
        return NoContent();
    }
    
    // DELETE: production/{id} 
    //deletes production with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteProductionById(int id)
    {
        // get production with the the given id
        var production = _unitOfWork.ProductionRepository.GetById(id);
        
        // check if production exists if not return a notFound response
        if (production == null)
        {
            return NotFound(new { message = "production not found" });
        }
        
        // start tracking 
        _unitOfWork.ProductionRepository.Remove(production);
        
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