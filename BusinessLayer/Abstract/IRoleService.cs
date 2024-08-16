using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface IRoleService
{
    void CreateRole(Role entity);
    void UpdateRole(Role entity);
    void DeleteRole(Role entity);
    Role GetRoleById(int id);
    List<Role> GetAllRole();
    Task<List<Role>> GetAllRoleAsync();
}

