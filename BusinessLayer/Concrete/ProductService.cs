using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _ProductRepository;

    public ProductService(IProductRepository ProductRepository)
    {
        _ProductRepository = ProductRepository;
    }

    public void CreateProduct(Product entity)
    {
        _ProductRepository.Insert(entity);
    }

    public void DeleteProduct(Product entity)
    {
        _ProductRepository.Delete(entity);
    }

    public List<Product> GetAllProduct()
    {
        return _ProductRepository.GetAll().ToList();
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _ProductRepository.GetAllAsync();
    }

    public List<Product> GetAllWithRelations()
    {
        return _ProductRepository.GetAllWithRelations(false, "Category").ToList();
    }

    public List<Product> GetFilteredProducts(FilterProducts filter)
    {
        var filteredProducts = _ProductRepository.GetAllWithRelations(false, "Category");
        if (filter.Ratings != null && filter.Ratings.Any())
        {
            filteredProducts = filteredProducts.Where(p => filter.Ratings.Contains(Convert.ToInt32(decimal.Ceiling(p.Rating))));
        }

        //TODO: Product-a color artiandan sonra service
        /*if (filter.Colors != null && filter.Colors.Any())
        {
            filteredProducts = filteredProducts.Where(p => filter.Colors.Any(c => c == p.Color.Name));
        }*/

        if (filter.MinPrice > 0 || filter.MaxPrice > 0)
        {
            filteredProducts = filteredProducts.Where(p => p.Price >= filter.MinPrice && p.Price <= filter.MaxPrice);
        }

        if (filter.OrderBy != null)
        {
            filteredProducts = filter.OrderBy switch
            {
                "bydate" => filteredProducts.OrderBy(p=>p.Id),
                //"byselling" => filteredProducts.OrderBy()
                "byrating" => filteredProducts.OrderBy(p => p.Rating),
                "lowest" => filteredProducts.OrderBy(p => p.Price),
                "highest" => filteredProducts.OrderBy(p => p.Price).Reverse(),
                _ => filteredProducts
            };
        }
        return filteredProducts.ToList();

    }

    public Product GetProductById(int id)
    {
        return _ProductRepository.GetById(id);
    }

    public Product GetProductWithRelations(int id)
    {
        return _ProductRepository.GetEntityWithRelations(id, false, "Category");
    }

    public void UpdateProduct(Product entity)
    {
        _ProductRepository.Update(entity);
    }
}


