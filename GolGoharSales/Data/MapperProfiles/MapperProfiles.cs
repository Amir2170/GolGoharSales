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