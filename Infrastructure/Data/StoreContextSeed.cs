

using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext){
            if(!(storeContext.productBrands.Any())){
                var branddata =File.ReadAllText("..\\Infrastructure\\Data\\SeedData\\brands.json");
                var brands= JsonSerializer.Deserialize<List<ProductBrand>>(branddata);
                storeContext.productBrands.AddRange(brands);
            }
            if(!(storeContext.productTypes.Any())){
                var typedata =File.ReadAllText("..\\Infrastructure\\Data\\SeedData\\types.json");
                var types= JsonSerializer.Deserialize<List<ProductType>>(typedata);
                storeContext.productTypes.AddRange(types);
            }
            if(!(storeContext.products.Any())){
                var productdata =File.ReadAllText("..\\Infrastructure\\Data\\SeedData\\products.json");
                var products= JsonSerializer.Deserialize<List<Product>>(productdata);
                storeContext.products.AddRange(products);
            }
            if(storeContext.ChangeTracker.HasChanges()) await storeContext.SaveChangesAsync();
        }
    }
}