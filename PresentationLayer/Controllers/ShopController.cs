using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresentationLayer.Models;
using PresentationLayer.Models.Product;

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
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;
        var products = _productService.GetAllWithRelations()
            .Select(p=> new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                IsInWishlist = _context.UserFavoriteProducts.Any(x=>x.ProductId == p.Id && x.UserId == userId)
            })
            .ToList();
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

    public IActionResult GetProductDetails(int id)
    {
        var product = _productService.GetProductWithRelations(id);
        if (product == null)
        {
            return NotFound();
        }

        return Json(new
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryName = product.Category.Name,

        });
    }

    public async Task<IActionResult> Wishlist()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        var favorites = await _context.UserFavoriteProducts
            .Where(x => x.UserId == user.Id)
            .Include(u=>u.Product)
            .ThenInclude(p=>p.Category)
            .Select(e => e.Product)
            .ToListAsync();
        return View(favorites);
    }


    public async Task<IActionResult> AddWishlist([FromRoute] int id)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;

        var favorite = await _context.UserFavoriteProducts
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.ProductId == id);

        if (favorite != null)
        {
            _context.UserFavoriteProducts.Remove(favorite);
        }

        else
        {
            favorite = new UserFavoriteProduct
            {
                UserId = userId,
                ProductId = id
            };

            _context.UserFavoriteProducts.Add(favorite);
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    public async Task<int> GetWishlistCount()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;

        var wishlistCount = await _context.UserFavoriteProducts
            .CountAsync(uf => uf.UserId == userId);

        return wishlistCount;
    }

}
