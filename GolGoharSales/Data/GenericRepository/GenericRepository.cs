using GolGoharSales.Data.AppContext;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GolGoharSales.Data.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly SalesAppContext _context;

    public GenericRepository(SalesAppContext context)
    {
        // Defining context using DI
        _context = context;
    }
    
    // Get all instance of TEntity
    public virtual IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }
    
    // Get entity instance by id if doesn't exists return --null--
    public virtual TEntity GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }
    
    // Update entity in database without calling savechanges()
    public virtual void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }
    
    // Remove entity from database without calling savechanges()
    public virtual void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
    
    // Track the changes we make to an entity
    public virtual void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
    
    // finalizing changes by calling savechanges() 
    //public void Save()
    //{
    //    _context.SaveChanges();
    //}
}