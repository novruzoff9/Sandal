using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using EntityLayer.DTOs.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresentationLayer.Models;
using PresentationLayer.Models.Product;
using System.Linq;

namespace PresentationLayer.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly UserManager<User> _userManager;
    private readonly SandalContext _context;

    public ShopController(IProductService productService, UserManager<User> userManager, SandalContext context, ICategoryService categoryService)
    {
        _productService = productService;
        _userManager = userManager;
        _context = context;
        _categoryService = categoryService;
    }

    public IActionResult Index([FromQuery] string? categoryName)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;
        var products = _productService.GetAllWithRelations()
            .ToList();

        var categoryIds = new List<int>();

        if (categoryName != null)
        {
            var selectedCategory = _categoryService.GetCategoryHashId(categoryName);
            categoryIds.Add(selectedCategory.Id);
            var subcategoryIds = _categoryService.GetAllCategory()
                .Where(x => x.ParentCategoryId == selectedCategory.Id)
                .Select(x => x.Id)
                .ToList();

            categoryIds.AddRange(subcategoryIds);


            products = products.Where(x => categoryIds.Contains(x.CategoryId))
                .ToList();
        }

        var productsvm = products
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsInWishlist = _context.UserFavoriteProducts.Any(x => x.ProductId == p.Id && x.UserId == userId)
            })
            .ToList();

        ShopViewModel shopViewModel = new()
        {
            Categories = _categoryService.GetAllCategory(),
            Products = productsvm
        };
        return View(shopViewModel);
    }

    public IActionResult FilterPartial()
    {
        return PartialView();
    }

    [HttpPost]
    public IActionResult ProductsByFilter([FromBody] FilterProducts filter, string? categoryName)
    {
        var userId = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]).Id;
        var filteredProducts = _productService
            .GetFilteredProducts(filter);


        var categoryIds = new List<int>();

        if (categoryName != null)
        {
            var selectedCategory = _categoryService.GetCategoryHashId(categoryName);
            categoryIds.Add(selectedCategory.Id);
            var subcategoryIds = _categoryService.GetAllCategory()
                .Where(x => x.ParentCategoryId == selectedCategory.Id)
                .Select(x => x.Id)
                .ToList();

            categoryIds.AddRange(subcategoryIds);


            filteredProducts = filteredProducts.Where(x => categoryIds.Contains(x.CategoryId))
                .ToList();
        }

        var filteredProductsVM = filteredProducts
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                IsInWishlist = _context.UserFavoriteProducts.Any(x => x.ProductId == p.Id && x.UserId == userId)
            })
         .ToList();

        return Json(filteredProductsVM);
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
            .Include(u => u.Product)
            .ThenInclude(p => p.Category)
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
