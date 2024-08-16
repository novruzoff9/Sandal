using EntityLayer.Concrete;
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
    Product GetProductById(int id);
    List<Product> GetAllProduct();
    Task<List<Product>> GetAllProductAsync();

    List<Product> GetAllWithRelations();
    Product GetProductWithRelations(int id);
}

