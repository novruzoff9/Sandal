using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class FAQService : IFAQService
{
    private readonly IFAQRepository _FAQRepository;

    public FAQService(IFAQRepository FAQRepository)
    {
        _FAQRepository = FAQRepository;
    }

    public void CreateFAQ(FAQ entity)
    {
        _FAQRepository.Insert(entity);
    }

    public void DeleteFAQ(FAQ entity)
    {
        _FAQRepository.Delete(entity);
    }

    public List<FAQ> GetAllFAQ()
    {
        return _FAQRepository.GetAll().OrderBy(x=>x.Category).ToList();
    }

    public async Task<List<FAQ>> GetAllFAQAsync()
    {
        return await _FAQRepository.GetAllAsync();
    }

    public FAQ GetFAQById(int id)
    {
        return _FAQRepository.GetById(id);
    }

    public void UpdateFAQ(FAQ entity)
    {
        _FAQRepository.Update(entity);
    }
}


