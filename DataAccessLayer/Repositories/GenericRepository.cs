using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly SandalContext _context;
    public GenericRepository(SandalContext context)
    {
        _context = context;
    }

    public void Delete(int id)
    {
        var value = _context.Set<T>().FirstOrDefault(x => x.Id == id);
        if (value != null)
        {
            _context.Set<T>().Remove(value);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception($"Bu id üzrə dəyər yoxdur {nameof(T)} - {id}");
        }
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T GetById(int id)
    {
        var value = _context.Set<T>().FirstOrDefault(x => x.Id == id);
        if (value != null)
        {
            return value;
        }
        else
        {
            throw new Exception($"Bu id üzrə dəyər yoxdur {nameof(T)} - {id}");
        }
    }

    public IEnumerable<T> ListByFilter(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().Where(filter) ?? throw new Exception("İstəyinizə uyğun məlumat tapılmadı");
    }

    public void Insert(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public T GetByFilter(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().FirstOrDefault(filter) ?? throw new Exception("İstəyinizə uyğun məlumat tapılmadı");
    }

    public IQueryable<T> GetAllWithRelations(bool isTracking = false, params string[] includes)
    {
        var queryable = _context.Set<T>().AsQueryable();

        if (!isTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        foreach (string include in includes)
        {
            queryable = queryable.Include(include);
        }

        return queryable;
    }

    public T GetEntityWithRelations(int id, bool isTracking = false, params string[] includes)
    {
        var query = this.GetAllWithRelations(isTracking, includes);
        return query.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Bu id üzrə dəyər yoxdur {nameof(T)} - {id}");
    }
}
