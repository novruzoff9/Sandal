using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _UserRepository;

    public UserService(IUserRepository UserRepository)
    {
        _UserRepository = UserRepository;
    }

    public void CreateUser(User entity)
    {
        _UserRepository.Insert(entity);
    }

    public void DeleteUser(User entity)
    {
        _UserRepository.Delete(entity);
    }

    public List<User> GetAllUser()
    {
        return _UserRepository.GetAll().ToList();
    }

    public async Task<List<User>> GetAllUserAsync()
    {
        return await _UserRepository.GetAllAsync();
    }

    public User GetUserById(int id)
    {
        return _UserRepository.GetById(id);
    }

    public void UpdateUser(User entity)
    {
        _UserRepository.Update(entity);
    }
}


