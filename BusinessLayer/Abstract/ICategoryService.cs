using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface ICategoryService
{
    void CreateCategory(Category entity);
    void UpdateCategory(Category entity);
    void DeleteCategory(Category entity);
    Category GetCategoryById(int id);
    List<Category> GetAllCategory();
    Task<List<Category>> GetAllCategoryAsync();

    List<Category> GetParentCategories();
    Category GetCategoryWithRelations(int id);
}

