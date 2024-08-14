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

public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
{
    public FAQRepository(SandalContext context) : base(context)
    {
    }
}


