using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface IUserService
{
    void CreateUser(User entity);
    void UpdateUser(User entity);
    void DeleteUser(User entity);
    User GetUserById(int id);
    List<User> GetAllUser();
    Task<List<User>> GetAllUserAsync();
}

