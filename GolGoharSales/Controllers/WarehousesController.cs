using System.Data;
using System.Net;
using AutoMapper;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolGoharSales.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    
    // initializing unit of work through injected context
    public WarehousesController(
        UnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    //GET: warehouses/
    // Get all warehouses
    [HttpGet]
    public IEnumerable<Warehouse> GetAllWarehouses()
    {
        return _unitOfWork.WarehouseRepository.GetAll();
    }
    
    //GET: warehouses/{id}
    // Get warehouse by id if not exists return 404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Warehouse> GetWarehouseById(int id)
    {
        var warehouse = _unitOfWork.WarehouseRepository.GetById(id);

        if (warehouse == null)
        {
            return NotFound(new { message = "warehouse with given id doesn't exists" });
        }
        
        // mapping original object to DTO to use in frontend
        var warehouseDTO = _mapper.Map<Warehouse, WarehouseDTO>(warehouse);

        return Ok(warehouseDTO);
    }
    
    //PUT: warehouses/{id}
    /* Update warehouse with id parameter to warehouse entity parameter
        return 204 no content result if successful
     */
    [HttpPut("{id}")]
    public IActionResult UpdateWarehouse(int id, WarehouseDTO warehouseUpdateDTO)
    {
        // checking if id is equal to warehouseUpdate id
        if (id != warehouseUpdateDTO.Id)
        {
            return BadRequest(new { message = "Id in url is not equal to id in request body" });
        }
        
        // get warehouse by id parameter
        var warehouse = _unitOfWork.WarehouseRepository.GetById(id);
        
        // check if warehouse exists
        if (warehouse == null)
        {
            return NotFound(new { message = "warehouse not found" });
        }
        
        // check if location with id given in request body exists
        var location = _unitOfWork.LocationRepository.GetById(
            warehouseUpdateDTO.LocationId
        );

        if (location == null)
        {
            return NotFound(
                new { message = "location with given id does not exists use a valid location id" }
            );
        }
        
        //update warehouse and map it to original model
        warehouse = _mapper.Map<WarehouseDTO, Warehouse>(warehouseUpdateDTO);

        //track changes
        _unitOfWork.WarehouseRepository.Update(warehouse);
        
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
    
    //POST: warehouses
    // Creating a warehouse 
    [HttpPost]
    public ActionResult<Warehouse> CreateWarehouse(WarehouseDTO warehouseNewDTO)
    {   
        // mapping dto to original model
        var warehouseNew = _mapper.Map<WarehouseDTO, Warehouse>(warehouseNewDTO);
        
        // do not add duplicate warehouse
        if (_unitOfWork.WarehouseRepository.WarehouseExists(warehouseNew))
        {
            return BadRequest(new { message = "warehouse already exists" });
        }
        
        // check if location in request body exists if not return a 404 not found
        var location = _unitOfWork.LocationRepository.GetById(warehouseNew.LocationId);

        if (location == null)
        {
            return NotFound(
                new { message = "location with given id does not exists use a valid location id" }
            );
        }

        // start tracking
        _unitOfWork.WarehouseRepository.Add(warehouseNew);
        
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
        
        // return 204 no content result if successful
        return NoContent();
    }
    
    // DELETE: warehouses/{id} 
    //deletes warehouse with the given id if successful return noContent result
    [HttpDelete("{id}")]
    public IActionResult DeleteWarehouseById(int id)
    {
        // get warehouse with the the given id
        var warehouse = _unitOfWork.WarehouseRepository.GetById(id);

        // check if warehouse exists if not return a notFound response
        if (warehouse == null)
        {
            return NotFound(new { message = "warehouse not found" });
        }

        // start tracking 
        _unitOfWork.WarehouseRepository.Remove(warehouse);

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