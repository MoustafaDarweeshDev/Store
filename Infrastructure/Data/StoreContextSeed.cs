using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context  , ILoggerFactory loggerfactory)
        {
            try
            {
                if (!context.ProductTypes.Any())
                {
                    var brandtype =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(brandtype);

                    foreach (var s in types)
                    {
                        context.ProductTypes.Add(s);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductBrands.Any())
                {
                    var brandData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach(var  s in brands)
                    {
                          context.ProductBrands.Add(s);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var poductData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(poductData);

                    foreach (var s in Products)
                    {
                         context.Products.Add(s);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
