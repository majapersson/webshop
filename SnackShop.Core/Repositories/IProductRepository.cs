using System;
using System.Collections.Generic;
using SnackShop.Core.Models;

namespace SnackShop.Core.Repositories
{
    public interface IProductRepository
    {
        ProductModel Get(string slug);

        List<ProductModel> GetAll();

        bool Add(ProductModel product);

        bool Edit(ProductModel product);

    }
}
