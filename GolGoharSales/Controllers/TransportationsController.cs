using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.UnitOfWork;
using GolGoharSales.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
}