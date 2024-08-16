using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _RoleRepository;

    public RoleService(IRoleRepository RoleRepository)
    {
        _RoleRepository = RoleRepository;
    }

    public void CreateRole(Role entity)
    {
        _RoleRepository.Insert(entity);
    }

    public void DeleteRole(Role entity)
    {
        _RoleRepository.Delete(entity);
    }

    public List<Role> GetAllRole()
    {
        return _RoleRepository.GetAll().ToList();
    }

    public async Task<List<Role>> GetAllRoleAsync()
    {
        return await _RoleRepository.GetAllAsync();
    }

    public Role GetRoleById(int id)
    {
        return _RoleRepository.GetById(id);
    }

    public void UpdateRole(Role entity)
    {
        _RoleRepository.Update(entity);
    }
}


