using EntityLayer.Base;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
    void Delete(T entity);
    IQueryable<T> GetAllWithRelations(bool isTracking = false, params string[] includes);
    T GetEntityWithRelations(int id, bool isTracking = false, params string[] includes);
    IEnumerable<T> GetAll();
    IEnumerable<T> ListByFilter(Expression<Func<T, bool>> filter);
    T GetByFilter(Expression<Func<T, bool>> filter);
    Task<List<T>> GetAllAsync();
    T GetById(int id);
}
