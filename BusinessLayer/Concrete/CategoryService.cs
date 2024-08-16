using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

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

    public async Task<List<Category>> GetAllCategoryAsync()
    {
        return await _CategoryRepository.GetAllAsync();
    }

    public Category GetCategoryById(int id)
    {
        return _CategoryRepository.GetById(id);
    }

    public Category GetCategoryWithRelations(int id)
    {
        var category = _CategoryRepository.GetEntityWithRelations(id, false, "ParentCategory", "SubCategories", "Products");
        return category;
    }

    public List<Category> GetParentCategories()
    {
        return _CategoryRepository.ListByFilter(x => x.ParentCategoryId == null).ToList();
    }

    public void UpdateCategory(Category entity)
    {
        _CategoryRepository.Update(entity);
    }
}


