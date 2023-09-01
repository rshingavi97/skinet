using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        //attributes
        public int StatusCode {get;set;}
        public string Message {get;set;}
        public ApiResponse(int statuscode, string message=null) //message is default parameter
        {
            StatusCode = statuscode;
            Message= message?? GetCustomizedMessage(statuscode);
        }
        public string GetCustomizedMessage(int statuscode)
        {
            string msg="";
            switch(statuscode)
            {
                case 400: msg="A bad request, you have made!"; break;
                case 401: msg="Authorized, you are not!"; break;
                case 404: msg="Resource found, it was not!";break;
                case 500: msg="Errors are the path to the dark side. Errors lead to anger and Anger leads to hate, Hate leads to failure in our life.";break;
                default: msg=""; break;
            }
            return msg;
        }
    }
}