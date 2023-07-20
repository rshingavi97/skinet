using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            if(_context is not null)
            {
                List<Product>records = await _context.Products.ToListAsync();
                return records;
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Database is down");

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if(_context is not null)
            {
                Product prod=await _context.Products.FindAsync(id);
                return prod;
            }
             else
                return StatusCode(StatusCodes.Status404NotFound, "This product is not available");

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