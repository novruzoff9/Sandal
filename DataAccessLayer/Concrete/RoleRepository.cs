using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(SandalContext context) : base(context)
    {
    }
}


