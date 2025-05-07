using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules.ProductModule;
using Shared.DTOs.Product;
using Shared.ENums;

namespace Services.Specifications
{
    public class ProductSpecification : BaseSpecifications<Product>
    {
        //To Get Product By Id
        public ProductSpecification(int id) : base(pro => pro.Id == id)
        {
            //Need to Include  ProductBrand & ProductType
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }

        //To Get All Product
        public ProductSpecification(ProductQueryParameters productQueryParameters) : base(CreateCriteria(productQueryParameters))
        {
            //Need to Include  ProductBrand & ProductType
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
           ApplySorting (productQueryParameters);
            ApplyPagination(productQueryParameters.PageSize,productQueryParameters.PageIndex);
        }

        private static Expression<Func<Product,bool>> CreateCriteria(ProductQueryParameters productQueryParameters)
        {
         return Prod =>
         (!productQueryParameters.BrandId.HasValue || Prod.BrandId == productQueryParameters.BrandId.Value) &&
         (!productQueryParameters.TypeId.HasValue || Prod.TypeId == productQueryParameters.TypeId.Value) &&
         (string.IsNullOrWhiteSpace(productQueryParameters.Search) ||
         Prod.Name.ToLower().Contains(productQueryParameters.Search.ToLower()));
        }
        private void ApplySorting(ProductQueryParameters productQueryParameters)
        {
            switch (productQueryParameters.productSortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
            }
        }
    }
}
