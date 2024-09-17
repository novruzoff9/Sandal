using EntityLayer.Concrete;
using EntityLayer.DTOs.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface IProductService
{
    void CreateProduct(Product entity);
    void UpdateProduct(Product entity);
    void DeleteProduct(Product entity);
    Task<Product> AddImage(Product entity, IFormFile file);
    Product GetProductById(int id);
    List<Product> GetAllProduct();
    Task<List<Product>> GetAllProductAsync();

    List<Product> GetAllWithRelations();
    Product GetProductWithRelations(int id);
    public List<Product> GetFilteredProducts(FilterProducts filter);
}

