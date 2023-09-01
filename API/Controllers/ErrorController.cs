using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Mvc; //included for Route property

namespace API.Controllers
{
    //Overriding the Route property here which came from BaseApiController
    [Route("errors/{statuscode}")] 
    public class ErrorController:BaseApiController
    {
        [ApiExplorerSettings(IgnoreApi =true)]
        public IActionResult Error(int statuscode)
        {
            return new ObjectResult(new ApiResponse(statuscode));
        }
    }
}
