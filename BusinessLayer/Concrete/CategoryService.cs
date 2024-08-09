using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryService(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }

    public void CreateCategory(Category entity)
    {
        _CategoryRepository.Insert(entity);
    }

    public void DeleteCategory(Category entity)
    {
        _CategoryRepository.Delete(entity);
    }

    public List<Category> GetAllCategory()
    {
        return _CategoryRepository.GetAll().ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _CategoryRepository.GetById(id);
    }

    public void UpdateCategory(Category entity)
    {
        _CategoryRepository.Update(entity);
    }
}


