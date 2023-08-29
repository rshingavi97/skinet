using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.OpenApi.Writers;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        IProductRepository _repo;
        private readonly IGenericRepository<Product> _repoProd;
        private readonly IGenericRepository<ProductType> _repoProdType;
        private readonly IGenericRepository<ProductBrand> _repoProdBrand;
        private readonly IMapper _mapper;

        public ProductsController(  IGenericRepository<Product> product, 
                                    IGenericRepository<ProductType> prodType,
                                    IGenericRepository<ProductBrand> prodBrand,
                                    IProductRepository repo,
                                    IMapper mapper)
                                    {
                                        _repoProd=product;
                                        _repoProdType=prodType;
                                        _repoProdBrand=prodBrand;
                                        _repo=repo;
                                        _mapper=mapper;
                                    }
        
       /* public ProductsController(IProductRepository repo)
        {
            _repo=repo;
        }*/
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
           //return Ok(await _repo.GetProducts()); //way 1=> normal repository
           //return Ok(await _repoProd.ListAll());  //way 2=>  generic repository
           var spec= new ProductWithTypesAndBrandsSpecification();
           var products = await _repoProd.ListAsync(spec);
           /*List<ProductToReturnDto> dtoProdList = products.Select(prod=>new ProductToReturnDto
                                                {
                                                    Id=prod.Id,
                                                    Name=prod.Name,
                                                    Description=prod.Description,
                                                    PictureUrl=prod.PictureUrl,
                                                    Price=prod.Price,
                                                    ProductBrand=prod.ProductBrand.Name,
                                                    ProductType=prod.ProductType.Name
                                                }).ToList(); */
           IReadOnlyList<ProductToReturnDto> dtoProdList = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
           return Ok(dtoProdList); //way 3=> generic with specificaiton

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //return Ok(await _repo.GetProduct(id)) //way 1=> normal repository
            //return Ok(await _repoProd.GetById(id)); //way 2=> generic repository
            var spec=new ProductWithTypesAndBrandsSpecification(id);//way 3=> generic with specificaiton
            var prod = await _repoProd.GetEntityWithSpec(spec);
            /*var dtoProduct = new ProductToReturnDto
                            {
                                Id=prod.Id,
                                Name=prod.Name,
                                Description=prod.Description,
                                PictureUrl=prod.PictureUrl,
                                Price=prod.Price,
                                ProductBrand=prod.ProductBrand.Name,
                                ProductType=prod.ProductType.Name
                            };*/ //Remove manuall mapping bet DTO and Entities
            //Use the AutoMapping
            var dtoProduct = _mapper.Map<Product,ProductToReturnDto>(prod);             
            return Ok(dtoProduct);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
             //return Ok(await _repo.GetProductBrands());
             return Ok(await _repoProdBrand.ListAll()); //generic repository
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            //return Ok(await _repo.GetProductTypes());
            return Ok( await _repoProdType.ListAll()); //generic repository
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