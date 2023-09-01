using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public ApiValidationErrorResponse():base(400) //400=> NotFound 
        {

        }
        public IEnumerable<string> Errors{get;set;}
    }
}
