using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T>:IGenericRepository<T> where T:BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context=context;
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> ListAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            //Returning only one record hence FirstOrDefaultAsync() is used
            return await ApplySpecification(spec).FirstOrDefaultAsync();

        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            //Reutrning a list of records hence ToListAsync() is used.
            return await ApplySpecification(spec).ToListAsync();
        }
        //Avoid duplication of codes, ApplySpecification() is implemented.
        private IQueryable<T> ApplySpecification(ISpecification<T>spec)
        {
         //To evaluate the given query, we have implemented SpecificationEvaluator
         //SpecificationEvaluator.GetQuery() method takes 2 inputs as following:
         //1.Entity i.e. Table Name hence _context.Set<T> is used, T would be table name
         //2.Where claues represented by SPEC i.e. specification.
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

    }
}