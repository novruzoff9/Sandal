using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllWithRelations();
            return View(products);
        }

        public IActionResult _Filters()
        {
            return PartialView();
        }

        public IActionResult _ProductsByFilter(FilterProducts filter)
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
    }
}
