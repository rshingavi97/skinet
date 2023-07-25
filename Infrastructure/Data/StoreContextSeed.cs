using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any()) //if given table is empty
            {
                var data=File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                context.ProductBrands.AddRange(brands);
            }
            if(!context.ProductTypes.Any())
            {
                var data=File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                context.ProductTypes.AddRange(types);
            }
            if(!context.Products.Any())
            {
                var data = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(data);
                context.Products.AddRange(products);
            }
            //so far Context has done all the changes inside the memory. 
            //Now, perform the actual storing of data in the database from memory.
            if(context.ChangeTracker.HasChanges())//if any changes has done for context
            {
                //then, save the changes in the actual database.
                await context.SaveChangesAsync();
            }
        }
    }
}