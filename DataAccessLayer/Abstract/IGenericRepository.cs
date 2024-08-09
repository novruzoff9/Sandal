using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
    void Delete(T entity);
    List<T> GetAll();
    T GetById(int id);
}
