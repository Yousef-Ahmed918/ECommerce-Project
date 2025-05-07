using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Modules.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class DBInitializer(StoredDbContext _dbContext) : IDataBaseInitializer
    {
        public async Task InitializerAsync()
        {
            //In Case Of Deployment
            //if ( (await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //await _dbContext.Database.MigrateAsync();
            //}

            //In case of Development
            try
            {

                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    if (brands is not null && brands.Any())
                    {
                        _dbContext.Set<ProductBrand>().AddRange(brands);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                if (!_dbContext.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                    if (data is not null && types.Any())
                    {
                        _dbContext.Set<ProductType>().AddRange(types);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                if (!_dbContext.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(data);
                    if (data is not null && products.Any())
                    {
                        _dbContext.Set<Product>().AddRange(products);
                        await _dbContext.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
