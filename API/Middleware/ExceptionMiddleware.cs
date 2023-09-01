using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware( RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env
                                  )
        {
            _next=next;
            _logger=logger;
            _env=env;
        }
        public async Task Invoke(HttpContext httpReqContext)
        {
            //Invoke is handling the exception hence Try..Catch is must
            try
            {
                //this block executes for every HTTP request
                await _next(httpReqContext);
                //in case exception generated then it would be catched 

            }
            catch(Exception ex)
            {
                //At first log the error inside Console through Logger
                //we can log the error inside Log file as well.
                _logger.LogError(ex,ex.Message); 
                //Prepare the response header and statuscode
                httpReqContext.Response.ContentType="application/json";
                int statuscode=(int)HttpStatusCode.InternalServerError;//500
                httpReqContext.Response.StatusCode=statuscode;
                //Now prepare the message
                //Inside Development mode, send the entire Stack trace of given exception
                string stackTrace="";
                if(_env.IsDevelopment())
                {
                    stackTrace=ex.StackTrace.ToString();
                }
                //create the JSON Response
                var response=new ApiException(statuscode,ex.Message, stackTrace);
                var options=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var responseJson=JsonSerializer.Serialize(response,options);
                //Send the response
                await httpReqContext.Response.WriteAsync(responseJson);

            }
        }

    }
}