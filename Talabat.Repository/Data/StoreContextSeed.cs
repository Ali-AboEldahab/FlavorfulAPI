using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            if (_dbContext.ProductBrands.Count() == 0)
            {
                string brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                List<ProductBrand>? brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    foreach (ProductBrand brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                } 
            }

            if (_dbContext.ProductCategories.Count() == 0)
            {
                string categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                List<ProductCategory>? categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    foreach (ProductCategory category in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Products.Count() == 0)
            {
                string productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                List<Product>? products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    foreach (Product product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                string deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
                List<DeliveryMethod>? deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count() > 0)
                {
                    foreach (DeliveryMethod deliveryMethod in deliveryMethods)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
