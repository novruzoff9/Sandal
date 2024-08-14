using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PresentationLayer.ViewComponents.Layout;

public class CatalogViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CatalogViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _categoryService.GetAllCategory();
        return View(categories);
    }
}
