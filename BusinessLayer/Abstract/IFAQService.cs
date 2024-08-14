using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface IFAQService
{
    void CreateFAQ(FAQ entity);
    void UpdateFAQ(FAQ entity);
    void DeleteFAQ(FAQ entity);
    FAQ GetFAQById(int id);
    List<FAQ> GetAllFAQ();
    Task<List<FAQ>> GetAllFAQAsync();
}

