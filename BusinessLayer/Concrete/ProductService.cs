using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
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

    public Product GetProductById(int id)
    {
        return _ProductRepository.GetById(id);
    }

    public void UpdateProduct(Product entity)
    {
        _ProductRepository.Update(entity);
    }
}


