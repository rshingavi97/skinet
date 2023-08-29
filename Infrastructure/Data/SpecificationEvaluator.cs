using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        //GetQuery is a static method, woule be called by Class name directly.
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            //Note: here, inputQuery is representing the Entity i.e. Database Table.
            //For example, inputQuery would be Product table
            var query=inputQuery;
            if(spec.Criteria!=null)
            {
                //Criteria represents the Where clauses
                //For example, To find the ProductType record along with Product, Criteria could be p=> p.ProductTypeId=id
                query=query.Where(spec.Criteria);
            }
            query=spec.Includes.Aggregate(query,(current,include)=>current.Include(include));
             //Include and include both are different.
             //Include is from Microsoft.EntityFrameworkCore and include is representing ISpecification.Include
             //Here, Include represents Include clauses like  .Include(p=>p.ProductBrand) .Include(p=>p.ProductType)
            return query;
        }

        
    }
   
}
