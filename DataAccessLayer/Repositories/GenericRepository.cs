using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
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
}
