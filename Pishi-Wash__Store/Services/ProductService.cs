﻿using Pishi_Wash__Store.Data.Db;

namespace Pishi_Wash__Store.Services
{
    public class ProductService
    {
        private readonly TradeContext _tradeContext;
        public ProductService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<Models.Product>> GetProducts()
        {
            List<Models.Product> products = new();
            var product = await _tradeContext.Products.ToListAsync();
            await _tradeContext.Pnames.ToListAsync();
            await _tradeContext.Pmanufacturers.ToListAsync();
            await Task.Run(() =>
            {
                foreach (var item in product)
                {
                    products.Add(new Models.Product
                    {
                        Image = item.ProductPhoto == string.Empty ? "picture.png" : item.ProductPhoto,
                        Title = item.ProductNameNavigation.ProductName,
                        Description = item.ProductDescription,
                        Manufacturer = item.ProductManufacturerNavigation.ProductManufacturer,
                        Price = item.ProductCost,
                        Discount = item.ProductDiscountAmount.Value,
                        Article = item.ProductArticleNumber
                    });
                }
            });
            return products;
        }
    }
}
