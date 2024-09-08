using BusinessLayer.Abstract;
using BusinessLayer.Helpers;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;
    private readonly IMemoryCache _memoryCache;

    public CategoryService(ICategoryRepository CategoryRepository, IMemoryCache memoryCache)
    {
        _CategoryRepository = CategoryRepository;
        _memoryCache = memoryCache;
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
        var list = new List<Category>();
        if (!_memoryCache.TryGetValue("AllCategories", out List<Category> _categories))
        {
            list = _CategoryRepository.GetAll().ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _memoryCache.Set("AllCategories", list, cacheOptions);
        }
        else
        {
            list = _categories;
        }

        return list;
        //return _CategoryRepository.GetAll().ToList();
    }

    public async Task<List<Category>> GetAllCategoryAsync()
    {
        return await _CategoryRepository.GetAllAsync();
    }

    public Category GetCategoryById(int id)
    {
        return _CategoryRepository.GetById(id);
    }

    public Category GetCategoryHashId(string hashId)
    {
        return _CategoryRepository.GetByFilter(x => x.Id == Convert.ToInt32(hashId.Decrypt()));
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


