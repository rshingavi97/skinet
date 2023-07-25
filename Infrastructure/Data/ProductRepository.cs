using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository:IProductRepository
    {
        private StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context=context;
        }
        public async Task<Product> GetProduct(int id)
        {
            var res = await _context.Products
                    .Include(p=>p.ProductBrand)
                    .Include(p=>p.ProductType)
                    .FirstOrDefaultAsync(p=>p.Id==id);
            return res;
        }
        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var res = await _context.Products
                        .Include(p=>p.ProductType)  //including respective ProductType object
                        .Include(p=>p.ProductBrand) //including respective ProductBrand object
                        .ToListAsync();
            return res;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            var res = await _context.ProductBrands.ToListAsync();
            return res;
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            var res = await _context.ProductTypes.ToListAsync();
            return res;
        }

    }
}