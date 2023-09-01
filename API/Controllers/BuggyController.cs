using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context=context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            //Generate the NotFound Error
            int id_which_is_not_present_in_db_for_generatingError=-23;
            var rec = _context.Products.Find(id_which_is_not_present_in_db_for_generatingError);
            if(rec==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            //server error means exception could be generated by any illegal operation.
            int id_which_is_not_present_in_db_for_generatingError=-23;
            var rec = _context.Products.Find(id_which_is_not_present_in_db_for_generatingError);
            var result= rec.ToString(); // generating the exception
            return Ok(result);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        
    }
}