using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Modules.ProductModule;
using Services.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.Product;
using Shared.ENums;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResponse>> GetAllBrandsAsync()
        {
            var repo=_unitOfWork.GetRepository<ProductBrand,int>(); //Igeneric repository
            var brands = await repo.GetAllAsync();
            var result= _mapper.Map<IEnumerable< BrandResponse>>(brands);
            return result;

        }

        public async Task<PaginatedResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters productQueryParameters)
        {
            var specs = new ProductSpecification(productQueryParameters);//No Filter With id 
           var products =await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specs);
           var productRes= _mapper.Map<IEnumerable<ProductResponse>>(products);
            var countSpec = new ProductCountSpecification(productQueryParameters);
            var productCount=await _unitOfWork.GetRepository<Product,int>().CountAsync(countSpec);
            var res = new PaginatedResponse<ProductResponse>()
            {
                Data = productRes,
                PageIndex = productQueryParameters.PageIndex,    
                PageSize = productQueryParameters.PageSize,
                TotalCount = productCount
            };
            return res;
        }

        public async Task<IEnumerable<TypeResponse>> GetAllTypesAsync()
        {
            var repo=_unitOfWork.GetRepository<ProductType, int>();
            var types=await repo.GetAllAsync();
            var result=_mapper.Map<IEnumerable<TypeResponse>>(types);
            return result;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var specs=new ProductSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs)??
                throw(new ProductNotFoundException(id));
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
