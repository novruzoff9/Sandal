using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Areas.Admin.Controllers;

[Area("admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public ActionResult Index()
    {
        var categories = _categoryService.GetAllCategory();
        return View(categories);
    }

    // GET: CategoriesController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    public ActionResult Create()
    {
        ViewBag.ParentCategoryId = new SelectList(_categoryService.GetAllCategory().Where(x => x.ParentCategoryId == null), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CategoriesController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: CategoriesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CategoriesController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: CategoriesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
