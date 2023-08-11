using AutoMapper;
using GolGoharSales.Models;

namespace GolGoharSales.Data.MapperProfiles;

// production mapper profile un-flattening and flattening
public class ProductionProfile: Profile
{
    public ProductionProfile()
    {
        CreateMap<Production, ProductionDTO>()
            .ReverseMap();
    }
}

// customer mapper profile un-flattening and flattening
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDTO>()
            .ReverseMap();
    }
}

// location mapper profile un-flattening and flattening 
public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDTO>()
            .ReverseMap();
    }
}

// SalesContract mapper profile un-flattening and flattening
public class SalesContractProfile : Profile
{
    public SalesContractProfile()
    {
        CreateMap<SalesContract, SaleContractDTO>()
            .ReverseMap();
    }
}

// Transportation mapper profile un-flattening and flattening
public class TransportationProfile : Profile
{
    public TransportationProfile()
    {
        CreateMap<Transportation, TransportationDTO>()
            .ReverseMap();
    }
}

// Warehouse mapper profile un-flattening and flattening
public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, WarehouseDTO>()
            .ReverseMap();
    }
}