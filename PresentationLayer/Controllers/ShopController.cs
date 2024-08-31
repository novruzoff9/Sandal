using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly SandalContext _context;

    public ShopController(IProductService productService, UserManager<User> userManager, SandalContext context)
    {
        _productService = productService;
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllWithRelations();
        return View(products);
    }

    public IActionResult ProductsByFilter(FilterProducts filter)
    {
        var products = _productService.GetAllWithRelations();
        if(filter.CategoryName != null)
        {
            products = products.Where(x => x.Category.Name == filter.CategoryName).ToList();
        }
        if (filter.MinPrice > 0)
        {
            products = products.Where(x => x.Price > filter.MinPrice).ToList();
        }
        if (filter.MaxPrice > 0)
        {
            products = products.Where(x => x.Price < filter.MaxPrice).ToList();
        }

        var jsonData = JsonConvert.SerializeObject(products);

        return Json(jsonData); 
    }

    public async Task<IActionResult> Wishlist()
    {
        var user = await _userManager.GetUserAsync(User);
        var favorites = await _context.UserFavoriteProducts
            .Where(x => x.UserId == user.Id)
            .Select(x => x.Product)
            .ToListAsync();
        return View(favorites);
    }
}
