using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.Product;
using Shared.ENums;

namespace Presentation.Controllers
{
        [Route("api/[controller]")]     //BaseUrl/api/products
        [ApiController]
    public class ProductController( IServiceManager _serviceManager):ControllerBase
    {
        //Get All Products
        [HttpGet]  //Get //BaseUrl/api/products
        public async Task< ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters productQueryParameters)
        {
            var products =await _serviceManager.ProductService.GetAllProductAsync(productQueryParameters);
            return Ok(products);
        } 
        //Get Product By Id 
        [HttpGet("{id}")] //Dynamic segment
        public async Task<ProductResponse> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return product;
        }

        //Get All Brands 
        [HttpGet("brands")] //Static segment
        public async Task<ActionResult <IEnumerable<BrandResponse>>> GetAllBrands()
        {
            var brands=await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
