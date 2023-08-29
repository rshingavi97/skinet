using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Specifications;
namespace Core.Specifications
{
    public class BaseSpecification<T>:ISpecification<T>
    {
            //having two properties
        public Expression<Func<T,bool>> Criteria {get;}
        // here, Includes property defines as having Null List Expression by default.
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        //What is the need of default constructor here?
        //Specification classes do not have any data members hence to allow 
        //their creation, defined the default constructor.
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T,bool>> criteria) 
        {
        //Through criteria, BaseSpecification object would be created
            Criteria=criteria;
        }

        protected void AddInclude(Expression<Func<T,object>>includeExpression)
        {
            Includes.Add(includeExpression); //It can hold the expressions.
        }
    }
    
}

