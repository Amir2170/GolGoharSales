using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolGoharSales.Data.GenericRepository;

// GenericRepository Interface 
public interface IGenericRepository<TEntity> where TEntity : class
{
    // Get all instance of TEntity
    IEnumerable<TEntity> GetAll();
    
    // Get entity instance by id
    TEntity GetById(int id);
    
    // Add entity to database without calling savechanges()
    void Add(TEntity entity);
    
    // Remove entity from database 
    void Remove(TEntity entity);
    
    // Update entity in database 
    void Update(TEntity entity);
    
    // finalizing changes by calling savechanges() 
    //void Save();
}