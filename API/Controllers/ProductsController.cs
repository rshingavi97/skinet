using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        IProductRepository _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo=repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products=await _repo.GetProducts();
            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProduct(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
             var brands = await _repo.GetProductBrands();
             return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _repo.GetProductTypes();
            return Ok(types);
        }

        /*[HttpGet]
        public IActionResult GetProducts()
        {
            if(_context is not null)
            {
                var records=_context.Products.ToList();
                return Ok(records);
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Database is down");
        }
        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var prod = _context.Products.Find(id);
            if(prod is not null)
                return Ok(prod);
            else
                return StatusCode(StatusCodes.Status404NotFound, "This product is not available");
            
        }*/
    }
}