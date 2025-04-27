using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Project.Controllers
{
    [Route("api/[controller]")] //BaseURL +api/ControllerName
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id) //Get =>   BaseURL +api/ControllerName?Id
        {
            return new Product { Id = id };
        }
        [HttpPut]
        public ActionResult<Product> Update() //Get =>   BaseURL +api/ControllerName
        {
            return new Product { Id = 15 };
        }
        [HttpDelete]
        public ActionResult <Product> Delete(Product product)
        {
            return new Product { Id = product.Id,Name=product.Name };

        }
    }

    public class Product {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
