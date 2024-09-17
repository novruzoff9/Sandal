using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;

namespace PresentationLayer.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    // GET: Admin/Products
    public IActionResult Index()
    {
        return View(_productService.GetAllWithRelations());
    }

    // GET: Admin/Products/Details/5
    public IActionResult Details(int id)
    {
        var product = _productService.GetProductWithRelations(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Admin/Products/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategory(), "Id", "Name");
        return View();
    }

    // POST: Admin/Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Name,Price,Rating,CategoryId,Id")] Product product)
    {
        if (ModelState.IsValid)
        {
            _productService.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategory(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    // GET: Admin/Products/Edit/5
    public IActionResult Edit(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategory(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    // POST: Admin/Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile file)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

       
            try
            {
                product = await _productService.AddImage(product, file);
                _productService.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        ViewData["CategoryId"] = new SelectList(_categoryService.GetAllCategory(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    // GET: Admin/Products/Delete/5
    public IActionResult Delete(int id)
    {

        var product = _productService.GetProductWithRelations(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Admin/Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _productService.GetProductById(id);
        if (product != null)
        {
            _productService.DeleteProduct(product);
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _productService.GetProductById(id) != null;
    }
}
