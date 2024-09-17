using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers.PhotoStock;

public interface IPhotoStockHelper
{
    public bool ProductImageToRoot(IFormFile file, Product product);
}

public class PhotoStockHelper : IPhotoStockHelper
{
    public bool ProductImageToRoot(IFormFile file, Product product)
    {
        string fileName = file.Name;


        return false;
    }
}
