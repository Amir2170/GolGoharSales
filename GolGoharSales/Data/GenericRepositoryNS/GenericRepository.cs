using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using GolGoharSales.Data.AppContext;


namespace GolGoharSales.Data.GenericRepositoryNS;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly SalesAppContext Context;

    public GenericRepository(SalesAppContext dbcontext)
    {
        // Defining context using DI
        Context = dbcontext;
    }
    
    // Get all instance of TEntity
    public virtual IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }
    
    // Get entity instance by id if doesn't exists return --null--
    public virtual TEntity GetById(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }
    
    // Update entity in database without calling savechanges()
    public virtual void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }
    
    // Remove entity from database without calling savechanges()
    public virtual void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
    
    // Track the changes we make to an entity
    public virtual void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }
    
    // finalizing changes by calling savechanges() 
    /*public void Save()
    {
        Context.SaveChanges();
    }*/
}