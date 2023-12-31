﻿using System;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Data.CustomerRepositoryNS;
using GolGoharSales.Data.LocationRepositoryNS;
using GolGoharSales.Data.ProductionRepositoryNS;
using GolGoharSales.Data.SalesContractRepositoryNS;
using GolGoharSales.Data.TransportationRepositoryNS;
using GolGoharSales.Data.WarehouseRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private readonly SalesAppContext _context;
    
    // Contract repository
    private SalesContractRepository? _salesContractRepository;
    
    // Customer repository
    private CustomerRepository? _customerRepository;
    
    // Location repository
    private LocationRepository? _locationRepository;
    
    // Production repository
    private ProductionRepository? _productionRepository;
    
    // Transportation repository
    private TransportationRepository? _transportationRepository;
    
    // Warehouse repository
    private WarehouseRepository? _warehouseRepository;
    
    // Constructor
    public UnitOfWork(SalesAppContext context)
    {
        _context = context;
    }
    
    // initializing and returning ContractRepository
    public SalesContractRepository SalesContractRepository
    {
        get
        {
            if (_salesContractRepository == null)
            {
                _salesContractRepository = new SalesContractRepository(_context);
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
    public LocationRepository LocationRepository
    {
        get
        {
            if (_locationRepository == null)
            {
                _locationRepository = new LocationRepository(_context);
            }

            return _locationRepository;
        }
    }
    
    // Initializing and returning ProductionRepository
    public ProductionRepository ProductionRepository
    {
        get
        {
            if (_productionRepository == null)
            {
                _productionRepository = new ProductionRepository(_context);
            }

            return _productionRepository;
        }
    }
    
    // Initializing and returning TransportationRepository
    public TransportationRepository TransportationRepository
    {
        get
        {
            if (_transportationRepository == null)
            {
                _transportationRepository = new TransportationRepository(_context);
            }

            return _transportationRepository;
        }
    }
    
    // Initializing and returning WarehouseRepository
    public WarehouseRepository WarehouseRepository
    {
        get
        {
            if (_warehouseRepository == null)
            {
                _warehouseRepository = new WarehouseRepository(_context);
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