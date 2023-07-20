using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepository;
using GolGoharSales.Models;

namespace GolGoharSales.Data.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private readonly SalesAppContext _context;
    
    // Contract repository
    private GenericRepository<Contract>? contractRepository;
    
    // Customer repository
    private GenericRepository<Customer>? customerRepository;
    
    // Location repository
    private GenericRepository<Location>? locationRepository;
    
    // Production repository
    private GenericRepository<Production>? productionRepository;
    
    // Transportation repository
    private GenericRepository<Transportation>? transportatonRepository;
    
    // Warehouse repository
    private GenericRepository<Warehouse>? warehouseRepository;
    
    // Constructor
    public UnitOfWork(SalesAppContext context)
    {
        _context = context;
    }
    
    // initializing and returning ContractRepository
    public GenericRepository<Contract> ContractRepository
    {
        get
        {
            if (contractRepository == null)
            {
                contractRepository = new GenericRepository<Contract>(_context);
            }

            return contractRepository;
        }
    }
    
    // Initializing and returning CustomerRepository
    public GenericRepository<Customer> CustomerRepository
    {
        get
        {
            if (customerRepository == null)
            {
                customerRepository = new GenericRepository<Customer>(_context);
            }

            return customerRepository;
        }
    }
    
    // Initializing and returning LocationRepository
    public GenericRepository<Location> LocationRepository
    {
        get
        {
            if (locationRepository == null)
            {
                locationRepository = new GenericRepository<Location>(_context);
            }

            return locationRepository;
        }
    }
    
    // Initializing and returning ProductionRepository
    public GenericRepository<Production> ProductionRepository
    {
        get
        {
            if (productionRepository == null)
            {
                productionRepository = new GenericRepository<Production>(_context);
            }

            return productionRepository;
        }
    }
    
    // Initializing and returning TransportationRepository
    public GenericRepository<Transportation> TransportationRepository
    {
        get
        {
            if (transportatonRepository == null)
            {
                transportatonRepository = new GenericRepository<Transportation>(_context);
            }

            return transportatonRepository;
        }
    }
    
    // Initializing and returning WarehouseRepository
    public GenericRepository<Warehouse> WarehouseRepository
    {
        get
        {
            if (warehouseRepository == null)
            {
                warehouseRepository = new GenericRepository<Warehouse>(_context);
            }

            return warehouseRepository;
        }
    }
    
    // Save method to call savechanges() on all repositories
    public void Save()
    {
        _context.SaveChanges();
    }

    //A boolean that indicates whether an unitofwork is disposed or not
    private bool _disposed = false;
    
    protected virtual void Dispose(bool disposing)
    {
        //if it is not disposed and suppose to be disposing then do it
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }
    
    // Main Dispose method
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}