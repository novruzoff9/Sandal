using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly SandalContext _context;

    public ProductController(IProductService productService, UserManager<User> userManager, SandalContext context)
    {
        _productService = productService;
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        var product = _productService.GetProductWithRelations(id);
        return View(product);
    }
}
