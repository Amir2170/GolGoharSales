using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Data.CustomerRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private readonly SalesAppContext _context;
    
    // Contract repository
    private GenericRepository<SalesContract>? _salesContractRepository;
    
    // Customer repository
    private CustomerRepository? _customerRepository;
    
    // Location repository
    private GenericRepository<Location>? _locationRepository;
    
    // Production repository
    private GenericRepository<Production>? _productionRepository;
    
    // Transportation repository
    private GenericRepository<Transportation>? _transportationRepository;
    
    // Warehouse repository
    private GenericRepository<Warehouse>? _warehouseRepository;
    
    // Constructor
    public UnitOfWork(SalesAppContext context)
    {
        _context = context;
    }
    
    // initializing and returning ContractRepository
    public GenericRepository<SalesContract> ContractRepository
    {
        get
        {
            if (_salesContractRepository == null)
            {
                _salesContractRepository = new GenericRepository<SalesContract>(_context);
            }

            return _salesContractRepository;
        }
    }
    
    // Initializing and returning CustomerRepository
    public CustomerRepository CustomerRepository
    {
        get
        {
            if (_customerRepository == null)
            {
                _customerRepository = new CustomerRepository(_context);
            }

            return _customerRepository;
        }
    }
    
    // Initializing and returning LocationRepository
    public GenericRepository<Location> LocationRepository
    {
        get
        {
            if (_locationRepository == null)
            {
                _locationRepository = new GenericRepository<Location>(_context);
            }

            return _locationRepository;
        }
    }
    
    // Initializing and returning ProductionRepository
    public GenericRepository<Production> ProductionRepository
    {
        get
        {
            if (_productionRepository == null)
            {
                _productionRepository = new GenericRepository<Production>(_context);
            }

            return _productionRepository;
        }
    }
    
    // Initializing and returning TransportationRepository
    public GenericRepository<Transportation> TransportationRepository
    {
        get
        {
            if (_transportationRepository == null)
            {
                _transportationRepository = new GenericRepository<Transportation>(_context);
            }

            return _transportationRepository;
        }
    }
    
    // Initializing and returning WarehouseRepository
    public GenericRepository<Warehouse> WarehouseRepository
    {
        get
        {
            if (_warehouseRepository == null)
            {
                _warehouseRepository = new GenericRepository<Warehouse>(_context);
            }

            return _warehouseRepository;
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