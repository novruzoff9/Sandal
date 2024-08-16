using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
        return View(_categoryService.GetCategoryWithRelations(id));
    }

    public ActionResult Create()
    {
        ViewBag.ParentCategoryId = new SelectList(_categoryService.GetParentCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Category category)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(category);
            }
            else
            {
                return View(category);
            }
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
        Category category = _categoryService.GetCategoryById(id);
        if (category.ParentCategoryId != null)
        {
            ViewBag.ParentCategoryId = new SelectList(_categoryService.GetParentCategories(), "Id", "Name");
        }
        else
        {
            var parentCategories = _categoryService.GetParentCategories();
            parentCategories.Remove(category);
            ViewBag.ParentCategoryId = new SelectList(parentCategories, "Id", "Name");
        }
        return View(category);
    }

    // POST: CategoriesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Category model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(model);
            }
            else
            {
                return View(model);
            }
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
        return View(_categoryService.GetCategoryById(id));
    }

    // POST: CategoriesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            var category = _categoryService.GetCategoryById(id);
            _categoryService.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
