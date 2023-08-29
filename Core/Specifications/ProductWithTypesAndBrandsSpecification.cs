using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {

        public ProductWithTypesAndBrandsSpecification() //indirectly calling default cons of BaseSpecification class
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
        //Getting used when fetching the particular record by its id
        public ProductWithTypesAndBrandsSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}