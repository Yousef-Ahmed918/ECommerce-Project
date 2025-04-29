using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
   public class StoredDbContext(DbContextOptions<StoredDbContext> options):DbContext(options)
    {
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)=>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoredDbContext).Assembly);

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        
    }
}
