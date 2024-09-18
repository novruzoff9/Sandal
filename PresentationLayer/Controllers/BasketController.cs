using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace PresentationLayer.Controllers;

public class BasketController : Controller
{
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SandalContext _context;

    public BasketController(IProductService productService, UserManager<User> userManager, SandalContext context, RoleManager<Role> roleManager)
    {
        _productService = productService;
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var currentuser = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        var user = await _userManager.FindByIdAsync(currentuser.Id);
        var basketitems = await _context.UserBasketProducts
            .Where(x => x.UserId == user.Id)
            .Include(u => u.Product)
            .ThenInclude(p => p.Category)
            .Select(e => e.Product)
            .ToListAsync();

        return View(basketitems);
    }


    public async Task<IActionResult> AddBasket([FromRoute] int id)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;

        var basketitems = await _context.UserBasketProducts
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.ProductId == id);

        if (basketitems != null)
        {
            _context.UserBasketProducts.Remove(basketitems);
        }

        else
        {
            basketitems = new UserBasketProduct
            {
                UserId = userId,
                ProductId = id
            };

            _context.UserBasketProducts.Add(basketitems);
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    public async Task<int> GetBasketCount()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        string userId = user.Id;

        var basketCount = await _context.UserFavoriteProducts
            .CountAsync(uf => uf.UserId == userId);

        return basketCount;
    }
}
