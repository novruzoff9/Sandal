using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PresentationLayer.ViewComponents;

public class CatalogViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CatalogViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryService.GetAllCategoryAsync();
        return View(categories);
    }
}
