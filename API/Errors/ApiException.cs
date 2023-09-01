using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException:ApiResponse
    {
        public string Details{get;set;}
        public ApiException(int statuscode, 
                            string message=null, 
                            string details=null
                            ):base(statuscode,message)
        {
            Details = details;
        }
    }
}