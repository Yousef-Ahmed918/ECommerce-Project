using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules.ProductModule;
using Shared.DTOs.Product;

namespace Services.Specifications
{
    public class ProductCountSpecification(ProductQueryParameters productQueryParameters)
        :BaseSpecifications<Product>(CreateCriteria(productQueryParameters))
    {
        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters productQueryParameters)
        {
            return Prod =>
            (!productQueryParameters.BrandId.HasValue || Prod.BrandId == productQueryParameters.BrandId.Value) &&
            (!productQueryParameters.TypeId.HasValue || Prod.TypeId == productQueryParameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(productQueryParameters.Search) ||
            Prod.Name.ToLower().Contains(productQueryParameters.Search.ToLower())); 
        }
    }
}
