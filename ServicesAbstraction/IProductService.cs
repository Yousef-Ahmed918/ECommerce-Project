using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DTOs.Product;
using Shared.ENums;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        //Get All Products
        Task<PaginatedResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters productQueryParameters);
        //Get Product By Id 
        Task<ProductResponse> GetProductByIdAsync(int id);
        //Get All Brands 
        Task<IEnumerable<BrandResponse>> GetAllBrandsAsync();
        //Get All Types
        Task<IEnumerable<TypeResponse>> GetAllTypesAsync();
    }
}
